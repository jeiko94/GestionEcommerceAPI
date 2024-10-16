
using GestionEcommerceAPI.DataAccess;
using GestionEcommerceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace GestionEcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly EcommerceContext _context;
        private readonly IConfiguration _configuration;

        //Constructor
        public AuthController(EcommerceContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //POST: api/Auth/register
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] UsuarioRegister model)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Username == model.Username))
            {
                return BadRequest("El nombre del usuario ya existe.");
            }

            var usuario = new Usuario
            {
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Username = model.Username,
                Password = model.Password //En producción, encriptar la contraseña
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok("usuario registrado exitosamente.");
        }

        //POST: api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin model)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);
            if (usuario == null)
            {
                return Unauthorized("Credenciales  invalidas.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("TuClaveSecretaMuyLargaParaSeguridad"); //Debe coincidir con la clave en Program.cs

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Username),
                    new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            var jwtToken = tokenHandler.WriteToken(token);

            return Ok(new { Token = jwtToken });
        }

        //Modelo de registro para usuarios
        public class UsuarioRegister
        {
            [Required]
            public string Nombre { get; set; }
            [Required]
            public string Apellido { get; set; }
            [Required]
            public string Username { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        //Modelo para inicion de sesion de usuarios
        public class UserLogin
        {
            [Required]
            public string Username { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}

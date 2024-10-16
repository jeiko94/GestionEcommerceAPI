//using Microsoft.AspNetCore.Components;

using GestionEcommerceAPI.DataAccess;
using GestionEcommerceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace GestionEcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductosController : ControllerBase
    {
       private readonly EcommerceContext _context;

        public ProductosController(EcommerceContext context)
        {
            _context = context;
        }

        //GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> ObtenerProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        //GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> ObtenerProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        //POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<Producto>> EnviarProducto(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerProducto), new { id = producto.ProductoId }, producto);
        }

        //PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ProductoId == id);
        }

    }
}

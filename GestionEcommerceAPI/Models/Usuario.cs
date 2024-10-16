using System.ComponentModel.DataAnnotations;

namespace GestionEcommerceAPI.Models
{
    //Representa los usuarios en el sistema, es como la tabla usuarios
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El nonbre de usuario es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El nombre del usuario no puede exceder los 50 caracteres")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        //Relación uno a muchos con pedidos
        public List<Pedido> Pedidos { get; set; }

        public Usuario()
        {
            Pedidos = new List<Pedido>();
        }
    }
}

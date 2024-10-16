using System.ComponentModel.DataAnnotations;

namespace GestionEcommerceAPI.Models
{
    //Representa un producto en el sistema
    public class Producto
    {
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre del producto no debe de tener mas de 100 caracteres")]
        public string Nombre { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe de ser mayor que cero")]
        public decimal Precio {  get; set; }

        //Relación muchos a muchos con pedidos
        public List<PedidoProducto> pedidoProductos { get; set; }

        public Producto()
        {
            pedidoProductos = new List<PedidoProducto>();
        }
    }
}

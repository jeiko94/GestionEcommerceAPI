using System.ComponentModel.DataAnnotations;

namespace GestionEcommerceAPI.Models
{
    //Representa un pedido realizado por el usuario
    public class Pedido
    {
        public int PedidoId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaPedido { get; set; }

        //Relación muchos a muchos con productos
        public List<PedidoProducto> pedidoProductos { get; set; }

        public Pedido()
        {
            pedidoProductos = new List<PedidoProducto>();
        }

    }
}

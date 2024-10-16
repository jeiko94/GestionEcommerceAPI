namespace GestionEcommerceAPI.Models
{
    //Representa la relación muchos a muchos entre Pedidos y Productos.
    public class PedidoProducto //Clase Intermedia PedidoProducto para la Relación Muchos a Muchos
    {

        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }
    }
}

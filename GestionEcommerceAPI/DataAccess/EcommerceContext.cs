using GestionEcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEcommerceAPI.DataAccess
{
    //Contexto de la base de datos para el e-commerce.
    public class EcommerceContext : DbContext
    {
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base (options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoProducto> PedidosProductos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Claves primarias
            modelBuilder.Entity<Usuario>().HasKey(u => u.UsuarioId);
            modelBuilder.Entity<Producto>().HasKey(p => p.ProductoId);
            modelBuilder.Entity<Pedido>().HasKey(p => p.PedidoId);
            modelBuilder.Entity<PedidoProducto>().HasKey(pp => new { pp.PedidoId, pp.ProductoId});

            //Relaciones
            modelBuilder.Entity<PedidoProducto>()
                .HasOne(pp => pp.Pedido)
                .WithMany(p => p.pedidoProductos)
                .HasForeignKey(pp => pp.PedidoId);

            modelBuilder.Entity<PedidoProducto>()
                .HasOne(pp => pp.Producto)
                .WithMany(p => p.pedidoProductos)
                .HasForeignKey(pp => pp.ProductoId);
        }

    }
}

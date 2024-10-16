using GestionEcommerceAPI.DataAccess;
using GestionEcommerceAPI.Models;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace GestionEcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PedidosController : ControllerBase
    {
        private readonly EcommerceContext _context;

        public PedidosController(EcommerceContext context)
        {
            _context = context;
        }

        //GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> ObtenerPedidos()
        {
            return await _context.Pedidos
                                 .Include(p => p.pedidoProductos)
                                 .ThenInclude(pp => pp.Producto)
                                 .ToListAsync();
        }

        //GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> ObtenerPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include (p => p.pedidoProductos)
                .ThenInclude (pp => pp.Producto)
                .FirstOrDefaultAsync(p => p.PedidoId == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        //POST: api/Pedidos
        [HttpPost]
        public async Task<ActionResult<Pedido>> EnviarPedido(Pedido pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pedido.FechaPedido = DateTime.UtcNow;
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObtenerPedido), new { id = pedido.PedidoId }, pedido);
        }

        //DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NotFound();
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(p => p.PedidoId == id);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VentaAPI.Models;

namespace VentaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleVentasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DetalleVentasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DetalleVentas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleVentaItem>>> GetDetalleVentas()
        {
            return await _context.DetalleVentas.ToListAsync();
        }

        // GET: api/DetalleVentas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleVentaItem>> GetDetalleVentaItem(long id)
        {
            var detalleVentaItem = await _context.DetalleVentas.FindAsync(id);

            if (detalleVentaItem == null)
            {
                return NotFound();
            }

            return detalleVentaItem;
        }

        // PUT: api/DetalleVentas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleVentaItem(long id, DetalleVentaItem detalleVentaItem)
        {
            if (id != detalleVentaItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(detalleVentaItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleVentaItemExists(id))
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

        // POST: api/DetalleVentas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetalleVentaItem>> PostDetalleVentaItem(DetalleVentaDTO detalleVentaDTO)
        {
            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.Id == detalleVentaDTO.ProductoId);

            if (producto == null)
            {
                return NotFound("Producto no encontrado.");
            }

            if (producto.Stock < detalleVentaDTO.Cantidad)
            {
                return BadRequest("Stock insuficiente.");
            }

            producto.Stock -= detalleVentaDTO.Cantidad;

            var detalleVenta = new DetalleVentaItem
            {
                ProductoId = detalleVentaDTO.ProductoId,
                VentaId = detalleVentaDTO.VentaId,
                Cantidad = detalleVentaDTO.Cantidad,
                Precio = producto.Precio, // Usar el precio del producto
                Total = detalleVentaDTO.Cantidad * producto.Precio // Calcular el total
            };

            _context.DetalleVentas.Add(detalleVenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetalleVentaItem", new { id = detalleVenta.Id }, detalleVenta);
        }

        // DELETE: api/DetalleVentas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleVentaItem(long id)
        {
            var detalleVentaItem = await _context.DetalleVentas.FindAsync(id);
            if (detalleVentaItem == null)
            {
                return NotFound();
            }

            _context.DetalleVentas.Remove(detalleVentaItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleVentaItemExists(long id)
        {
            return _context.DetalleVentas.Any(e => e.Id == id);
        }

        [HttpGet("venta/{ventaId}")]
        public async Task<IActionResult> ObtenerDetallesPorVenta(long ventaId)
        {
            var ventaItem = await _context.Ventas.FindAsync(ventaId);

            if (ventaItem == null)
            {
                return NotFound();
            }

            var detalles = await _context.DetalleVentas
                .Where(dv => dv.VentaId == ventaId)
                .ToListAsync();

            if (detalles == null || detalles.Count == 0)
            {
                return NotFound("No se encontraron detalles para esta venta.");
            }

            var productoIds = detalles.Select(dv => dv.ProductoId).ToList();
            var productos = await _context.Productos
                .Where(p => productoIds.Contains(p.Id))
                .ToListAsync();

            var detallesVenta = detalles.Select(dv => new
            {
                dv.Id,
                Producto = productos.FirstOrDefault(p => p.Id == dv.ProductoId)?.Nombre,
                dv.Cantidad,
                dv.Precio,
                dv.Total
            }).ToList();

            var resultado = new
            {
                Venta = new
                {
                    ventaItem.Id,
                    fecha = ventaItem.Fecha.ToString("yyyy-MM-dd"),
                    hora = ventaItem.Fecha.ToString("HH:mm:ss"),
                    ventaItem.MetodoPago,
                    ventaItem.Total,
                    detallesVenta
                }
            };

            return Ok(resultado);
        }

    }
}

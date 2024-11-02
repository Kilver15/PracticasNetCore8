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
    public class VentasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VentasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Ventas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentaItem>>> GetVentas()
        {
            return await _context.Ventas.ToListAsync();
        }

        // GET: api/Ventas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VentaItem>> GetVentaItem(long id)
        {
            var ventaItem = await _context.Ventas.FindAsync(id);

            if (ventaItem == null)
            {
                return NotFound();
            }

            return ventaItem;
        }

        // PUT: api/Ventas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVentaItem(long id, VentaItem ventaItem)
        {
            if (id != ventaItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(ventaItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentaItemExists(id))
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

        // POST: api/Ventas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VentaItem>> PostVentaItem(VentaItem ventaItem)
        {
            _context.Ventas.Add(ventaItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVentaItem", new { id = ventaItem.Id }, ventaItem);
        }

        // DELETE: api/Ventas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVentaItem(long id)
        {
            var ventaItem = await _context.Ventas.FindAsync(id);
            if (ventaItem == null)
            {
                return NotFound();
            }

            _context.Ventas.Remove(ventaItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VentaItemExists(long id)
        {
            return _context.Ventas.Any(e => e.Id == id);
        }
    }
}

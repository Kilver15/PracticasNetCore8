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
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoItem>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoItem>> GetProductoItem(long id)
        {
            var productoItem = await _context.Productos.FindAsync(id);

            if (productoItem == null)
            {
                return NotFound();
            }

            return productoItem;
        }

        // PUT: api/Productos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductoItem(long id, ProductoItem productoItem)
        {
            if (id != productoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(productoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoItemExists(id))
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

        // POST: api/Productos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductoItem>> PostProductoItem(ProductoItem productoItem)
        {
            _context.Productos.Add(productoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductoItem", new { id = productoItem.Id }, productoItem);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductoItem(long id)
        {
            var productoItem = await _context.Productos.FindAsync(id);
            if (productoItem == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(productoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoItemExists(long id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}

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
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaItem>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaItem>> GetCategoriaItem(long id)
        {
            var categoriaItem = await _context.Categorias.FindAsync(id);

            if (categoriaItem == null)
            {
                return NotFound();
            }

            return categoriaItem;
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoriaItem(long id, CategoriaItem categoriaItem)
        {
            if (id != categoriaItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoriaItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaItemExists(id))
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

        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoriaItem>> PostCategoriaItem(CategoriaItem categoriaItem)
        {
            _context.Categorias.Add(categoriaItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoriaItem", new { id = categoriaItem.Id }, categoriaItem);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoriaItem(long id)
        {
            var categoriaItem = await _context.Categorias.FindAsync(id);
            if (categoriaItem == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoriaItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaItemExists(long id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
    }
}

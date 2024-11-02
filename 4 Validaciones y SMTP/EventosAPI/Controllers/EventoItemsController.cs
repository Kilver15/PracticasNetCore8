using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventosAPI.Models;
using EventosAPI.Services;

namespace EventosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoItemsController : ControllerBase
    {
        private readonly EventoContext _context;

        public EventoItemsController(EventoContext context)
        {
            _context = context;
        }

        // GET: api/EventoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventoItem>>> GetEventos()
        {
            return await _context.Eventos.ToListAsync();
        }

        // GET: api/EventoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventoItem>> GetEventoItem(long id)
        {
            var eventoItem = await _context.Eventos.FindAsync(id);

            if (eventoItem == null)
            {
                return NotFound();
            }

            return eventoItem;
        }

        // PUT: api/EventoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventoItem(long id, EventoItem eventoItem)
        {
            if (id != eventoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoItemExists(id))
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

        // POST: api/EventoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EventoItem>> PostEventoItem(EventoItem eventoItem)
        {
            _context.Eventos.Add(eventoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventoItem", new { id = eventoItem.Id }, eventoItem);
        }

        // DELETE: api/EventoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventoItem(long id)
        {
            var eventoItem = await _context.Eventos.FindAsync(id);
            if (eventoItem == null)
            {
                return NotFound();
            }

            _context.Eventos.Remove(eventoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventoItemExists(long id)
        {
            return _context.Eventos.Any(e => e.Id == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotasApi.Models;

namespace TodoApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly NotasContext _context;

    public TodoItemsController(NotasContext context)
    {
        _context = context;
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<NotasItemDTO>>> GetNotasItems()
    {
        return await _context.NotasItems
            .Select(x => ItemToDTO(x))
            .ToListAsync();
    }

    // GET: api/TodoItems/5
    // <snippet_GetByID>
    [HttpGet("{id}")]
    public async Task<ActionResult<NotasItemDTO>> GetNotasItem(long id)
    {
        var notasItem = await _context.NotasItems.FindAsync(id);

        if (notasItem == null)
        {
            return NotFound();
        }

        return ItemToDTO(notasItem);
    }
    // </snippet_GetByID>

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Update>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutNotasItem(long id, NotasItemDTO notasDTO)
    {
        if (id != notasDTO.Id)
        {
            return BadRequest();
        }

        var notasItem = await _context.NotasItems.FindAsync(id);
        if (notasItem == null)
        {
            return NotFound();
        }

        notasItem.Titulo = notasDTO.Titulo;
        notasItem.Detalle = notasDTO.Detalle;
        notasItem.UpdatedAt = notasDTO.UpdatedAt;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException) when (!NotasItemExists(id))
        {
            return NotFound();
        }

        return NoContent();
    }
    // </snippet_Update>

    // POST: api/TodoItems
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    // <snippet_Create>
    [HttpPost]
    public async Task<ActionResult<NotasItemDTO>> PostNotasItem(NotasItemDTO notasDTO)
    {
        var notasItem = new NotasItem
        {
            Titulo = notasDTO.Titulo,
            Detalle = notasDTO.Detalle
        };

        _context.NotasItems.Add(notasItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetNotasItem),
            new { id = notasItem.Id },
            ItemToDTO(notasItem));
    }
    // </snippet_Create>

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotasItem(long id)
    {
        var notasItem = await _context.NotasItems.FindAsync(id);
        if (notasItem == null)
        {
            return NotFound();
        }

        _context.NotasItems.Remove(notasItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool NotasItemExists(long id)
    {
        return _context.NotasItems.Any(e => e.Id == id);
    }

    private static NotasItemDTO ItemToDTO(NotasItem notasItem) =>
       new NotasItemDTO
       {
           Id = notasItem.Id,
           Titulo = notasItem.Titulo,
           Detalle = notasItem.Detalle
       };
}
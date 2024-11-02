using Microsoft.EntityFrameworkCore;
using EventosAPI.Models;

namespace EventosAPI.Models
{
    public class EventoContext : DbContext
    {
        public EventoContext(DbContextOptions<EventoContext> options)
            : base(options)
        {
        }

        public DbSet<EventoItem> Eventos { get; set; }
    }
  
}

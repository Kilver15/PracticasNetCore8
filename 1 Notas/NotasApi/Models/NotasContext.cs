using Microsoft.EntityFrameworkCore;
using NotasApi.Models;

namespace NotasApi.Models
{
    public class NotasContext : DbContext
    {
        public NotasContext(DbContextOptions<NotasContext> options)
        : base(options)
        {
            
        }

        public DbSet<NotasItem> NotasItems { get; set; } = null!;
        public DbSet<NotasApi.Models.NotasItemDTO> NotasItemDTO { get; set; } = default!;
    }
}

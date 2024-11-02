using Microsoft.EntityFrameworkCore;

namespace WebSocketAPI.Models
{
    public class WSDbContext : DbContext
    {
        public WSDbContext(DbContextOptions<WSDbContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
    }

}

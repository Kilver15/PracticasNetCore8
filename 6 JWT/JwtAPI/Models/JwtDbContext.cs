using Microsoft.EntityFrameworkCore;

namespace JwtAPI.Models
{
    public class JwtDbContext : DbContext
    {
        public JwtDbContext(DbContextOptions<JwtDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}

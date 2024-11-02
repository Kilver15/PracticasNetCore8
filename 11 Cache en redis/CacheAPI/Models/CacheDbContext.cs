using Microsoft.EntityFrameworkCore;

namespace CacheAPI.Models
{
    public class CacheDbContext : DbContext
    {
        public CacheDbContext(DbContextOptions<CacheDbContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}

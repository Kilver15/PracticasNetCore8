using Microsoft.EntityFrameworkCore;

namespace TestAPI.Models
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {

        }
         public DbSet<Person> Persons { get; set; }
    }
}

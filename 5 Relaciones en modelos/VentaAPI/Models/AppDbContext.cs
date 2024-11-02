using Microsoft.EntityFrameworkCore;

namespace VentaAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<ProductoItem> Productos { get; set; }
        public DbSet<CategoriaItem> Categorias { get; set; }
        public DbSet<VentaItem> Ventas { get; set; }
        public DbSet<DetalleVentaItem> DetalleVentas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductoItem>()
                .HasOne<CategoriaItem>()
                .WithMany()
                .HasForeignKey(p => p.CategoriaId);

            modelBuilder.Entity<DetalleVentaItem>()
            .HasOne<VentaItem>()
            .WithMany()
            .HasForeignKey(d => d.VentaId);

            modelBuilder.Entity<DetalleVentaItem>()
                .HasOne<ProductoItem>()
                .WithMany()
                .HasForeignKey(d => d.ProductoId);
            }
        }
}

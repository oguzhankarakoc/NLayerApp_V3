using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System.Reflection;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ProductFeature>().HasData(
                new ProductFeature() { Id = 1, Colour = "Kırmızı", Heigth = 100, Width = 10, ProductId = 1 },
                new ProductFeature() { Id = 2, Colour = "Turuncu", Heigth = 400, Width = 900, ProductId = 2 },
                new ProductFeature() { Id = 3, Colour = "Beyaz", Heigth = 300, Width = 700, ProductId = 3 },
                new ProductFeature() { Id = 4, Colour = "Mavi", Heigth = 50, Width = 60, ProductId = 4 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}

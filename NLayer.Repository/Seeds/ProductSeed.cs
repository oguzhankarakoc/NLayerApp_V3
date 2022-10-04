using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
            new Product
            { Name = "Kalem1", Id = 1, CategoryId = 1, Price = 100, Stock = 20, CreatedDate = DateTime.Now },
            new Product
            { Name = "Kitap1", Id = 2, CategoryId = 2, Price = 400, Stock = 50, CreatedDate = DateTime.Now },
            new Product
            { Name = "Defter1", Id = 3, CategoryId = 3, Price = 200, Stock = 10, CreatedDate = DateTime.Now },
            new Product
            { Name = "Kalem2", Id = 4, CategoryId = 1, Price = 300, Stock = 40, CreatedDate = DateTime.Now });
        }
    }
}

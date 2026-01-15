using Alza.Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alza.Products.Infrastructure.Context
{
    public static class ProductDbSeeder
    {
        public static async Task SeedAsync(ProductDbContext dbContext)
        {
            // only seed if table is empty
            if (await dbContext.Products.AnyAsync())
                return;

            // TODO: maybe change to loading mock data from a file? 
            var products = Enumerable.Range(1, 20).Select(i => new Product
            (
                Guid.NewGuid(),
                $"Product {i}",
                "https://example.com/image.png",
                 i * 10,
                "Seeded product"
            ));

            await dbContext.Products.AddRangeAsync(products);
            await dbContext.SaveChangesAsync();
        }
    }
}

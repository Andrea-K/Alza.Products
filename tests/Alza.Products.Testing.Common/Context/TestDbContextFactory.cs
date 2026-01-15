using Alza.Products.Domain.Entities;
using Alza.Products.Infrastructure.Context;
using Alza.Products.Testing.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Alza.Products.Testing.Common.Context
{
    public static class TestDbContextFactory
    {
        public static ProductDbContext CreateWithSeedData(string jsonFileName)
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ProductDbContext(options);

            SeedFromJson(context, jsonFileName);

            return context;
        }

        private static void SeedFromJson(ProductDbContext context, string jsonFileName)
        {
            var jsonPath = Path.Combine(AppContext.BaseDirectory, "TestData", jsonFileName);
            var json = File.ReadAllText(jsonPath);
            var seedModels = JsonSerializer.Deserialize<List<TestProductSeedModel>>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true})!;
            var products = seedModels.Select(x => new Product
            (
                x.Id,
                x.Name,
                x.ImgUri,
                x.Price,
                x.Description
            ));

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}

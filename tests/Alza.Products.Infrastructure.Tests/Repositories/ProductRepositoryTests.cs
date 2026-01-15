using Alza.Products.Infrastructure.Repositories;
using Alza.Products.Testing.Common.Context;
using Microsoft.EntityFrameworkCore;

namespace Alza.Products.Infrastructure.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            // Arrange
            using var dbContext = TestDbContextFactory.CreateWithSeedData("products.json");
            var repository = new ProductRepository(dbContext);

            // Act
            var products = await repository.GetAllProductsAsync();

            // Assert
            Assert.Equal(20, products.Count());

            var productNames = products.Select(p => p.Name).ToList();

            Assert.Contains("Sriracha Hot Chili Sauce", productNames);
            Assert.Contains("Sliced Provolone Cheese", productNames);
        }

        [Fact]
        public async Task GetAllProductsPagedAsync_ShouldReturnCorrectPage()
        {
            // Arrange
            using var dbContext = TestDbContextFactory.CreateWithSeedData("products.json");
            var repository = new ProductRepository(dbContext);

            // Act
            var products = await repository.GetAllProductsPagedAsync(page: 2, pageSize: 3);

            // Assert
            Assert.Equal(3, products.Count());

            var productNames = products.Select(p => p.Name).ToList();

            Assert.Contains("Garlic Parmesan Roasted Nuts", productNames);
            Assert.Contains("Buffalo Cauliflower Bites", productNames);
            Assert.Contains("Toasted Coconut Granola", productNames);

            Assert.DoesNotContain("Sriracha Hot Chili Sauce", productNames);
            Assert.DoesNotContain("Sliced Provolone Cheese", productNames);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnCorrectProduct()
        {
            // Arrange
            var knownId = Guid.Parse("e5a5262c-b9c1-48b7-a7be-a30199dfba0a");
            using var dbContext = TestDbContextFactory.CreateWithSeedData("products.json");
            var repository = new ProductRepository(dbContext);

            // Act
            var product = await repository.GetProductByIdAsync(knownId);

            // Assert
            Assert.NotNull(product);
            Assert.Equal("Sriracha Hot Chili Sauce", product!.Name);
        }

        [Fact]
        public async Task GetTotalCountAsync_ShouldReturnCount()
        {
            // Arrange
            using var context = TestDbContextFactory.CreateWithSeedData("products.json");
            var repository = new ProductRepository(context);

            // Act
            var count = await repository.GetTotalCountAsync();

            // Assert
            Assert.Equal(20, count);
        }
    }
}

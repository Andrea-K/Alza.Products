using Alza.Products.Application.DTOs;
using Alza.Products.Application.Exceptions;
using Alza.Products.Application.Interfaces;
using Alza.Products.Application.Services;
using Alza.Products.Domain.Entities;
using Moq;

namespace Alza.Products.Application.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductsRepository;
        private readonly IProductService _productService;

        public ProductServiceTests()
        {
            _mockProductsRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockProductsRepository.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShoulReturnAllProducts()
        {
            // Arrange
            var testProducts = GetTestProducts();

            _mockProductsRepository
                .Setup(r => r.GetAllProductsAsync())
                .ReturnsAsync(testProducts);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetProductByIdAsync_ExistingProduct_ShoulReturnCorrectProduct()
        {
            // Arrange
            var productId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var testProduct = GetTestProduct1();

            _mockProductsRepository
                .Setup(r => r.GetProductByIdAsync(productId))
                .ReturnsAsync(testProduct);

            // Act
            var result = await _productService.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
            Assert.Equal(testProduct.Name, result.Name);
        }

        [Fact]
        public async Task GetProductByIdAsync_ProductNotFound_ShouldThrowEntityNotFoundException()
        {
            // Arrange
            var wrongProductId = Guid.Parse("00000000-0000-0000-0000-000000009999");
            var testProduct = GetTestProduct1();

            _mockProductsRepository
                .Setup(r => r.GetProductByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Product?)null);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() =>
                _productService.GetProductByIdAsync(wrongProductId));
        }

        [Fact]
        public async Task UpdateProductDescriptionAsync_ShouldUpdateDescription()
        {
            // Arrange
            var productId = Guid.Parse("00000000-0000-0000-0000-000000009999");
            var testProduct = GetTestProduct1();
            var dto = new UpdateProductDescriptionDto
            {
                Description = "Updated description"
            };

            _mockProductsRepository
                .Setup(r => r.GetProductByIdAsync(productId))
                .ReturnsAsync(testProduct);

            _mockProductsRepository
                .Setup(r => r.UpdateProductDescriptionAsync(testProduct, dto.Description))
                .Returns(Task.CompletedTask);

            // Act
            var task = _productService.UpdateProductDescriptionAsync(productId, dto.Description);

            // Assert
            Assert.True(task.IsCompletedSuccessfully);
        }

        [Fact]
        public async Task UpdateProductDescriptionAsync_ProductNotFound_ShouldThrowEntityNotFoundException()
        {
            // Arrange
            var wrongProductId = Guid.Parse("00000000-0000-0000-0000-000000009999");
            var dto = new UpdateProductDescriptionDto
            {
                Description = "Updated description"
            };

            _mockProductsRepository
                .Setup(r => r.GetProductByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Product?)null);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() =>
                _productService.UpdateProductDescriptionAsync(wrongProductId, dto.Description));
        }

        #region setup
        private IEnumerable<Product> GetTestProducts()
        {
            return new List<Product>()
            {
                GetTestProduct1(),
                GetTestProduct2()
            };
        }

        private Product GetTestProduct1()
        {
            var productId = Guid.Parse("00000000-0000-0000-0000-000000000001");

            return new Product
            (
                id: productId,
                name: "Product1",
                imgUri: "https://example.com/product1.png",
                price: 199,
                description: "This is product 1."
            );
        }

        private Product GetTestProduct2()
        {
            var productId = Guid.Parse("00000000-0000-0000-0000-000000000002");

            return new Product
            (
                id: productId,
                name: "Product2",
                imgUri: "https://example.com/product2.png",
                price: 299,
                description: "This is product 2."
            );
        }
        #endregion
    }
}
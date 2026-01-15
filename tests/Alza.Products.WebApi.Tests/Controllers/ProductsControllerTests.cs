using Alza.Products.Application.DTOs;
using Alza.Products.Application.Interfaces;
using Alza.Products.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Alza.Products.WebApi.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductsController(_mockProductService.Object);
        }

        #region GetAllProductsAsync
        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnOk_WithAllProducts()
        {
            // Arrange
            var testProducts = GetTestProductDtos();

            _mockProductService
                .Setup(s => s.GetAllProductsAsync())
                .ReturnsAsync(testProducts);

            // Act
            var result = await _controller.GetAllProductsAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(testProducts, okResult.Value);

            //result.Result.Should().BeOfType<OkObjectResult>();
            //var okResult = result.Result as OkObjectResult;
            //okResult!.Value.Should().BeEquivalentTo(testProducts);
        }
        #endregion

        #region GetAllProductsPagedAsync
        [Fact]
        public async Task GetAllProductsPagedAsync_ShouldReturnOk_WithDefaultPaging()
        {
            // Arrange
            var testPagedResult = GetTestPagedResult(1, 10, 25);

            _mockProductService
                .Setup(s => s.GetAllProductsPagedAsync(1, 10))
                .ReturnsAsync(testPagedResult);

            // Act
            var result = await _controller.GetAllProductsPagedAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(testPagedResult, okResult.Value);
        }

        [Fact]
        public async Task GetAllProductsPagedAsync_ShouldReturnOk_WithCustomPaging()
        {
            // Arrange
            var testPagedResult = GetTestPagedResult(2, 5, 12);

            _mockProductService
                .Setup(s => s.GetAllProductsPagedAsync(2, 5))
                .ReturnsAsync(testPagedResult);

            // Act
            var result = await _controller.GetAllProductsPagedAsync(2, 5);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(testPagedResult, okResult.Value);
        }
        #endregion

        #region GetProductByIdAsync
        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnOk()
        {
            // Arrange
            var testProduct = GetTestProductDto1();

            _mockProductService
                .Setup(s => s.GetProductByIdAsync(testProduct.Id))
                .ReturnsAsync(testProduct);

            // Act
            var result = await _controller.GetProductByIdAsync(testProduct.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(testProduct, okResult.Value);
        }
        #endregion

        #region UpdateProductDescriptionAsync
        [Fact]
        public async Task UpdateProductDescriptionAsync_ShouldReturnNoContent()
        {
            // Arrange
            var testProductId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var dto = new UpdateProductDescriptionDto
            {
                Description = "Updated description"
            };

            _mockProductService
                .Setup(s => s.UpdateProductDescriptionAsync(testProductId, dto.Description))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateProductDescriptionAsync(testProductId, dto);

            // Assert
            var okResult = Assert.IsType<NoContentResult>(result);
        }
        #endregion

        #region setup
        private IEnumerable<ProductDto> GetTestProductDtos()
        {
            return new List<ProductDto>()
            {
                GetTestProductDto1(),
                GetTestProductDto2()
            };
        }

        private ProductDto GetTestProductDto1()
        {
            var productId = Guid.Parse("00000000-0000-0000-0000-000000000001");

            return new ProductDto
            (
                Id: productId,
                Name: "Product1",
                ImgUri: "https://example.com/product1.png",
                Price: 199,
                Description: "This is product 1."
            );
        }

        private ProductDto GetTestProductDto2()
        {
            var productId = Guid.Parse("00000000-0000-0000-0000-000000000002");

            return new ProductDto
            (
                Id: productId,
                Name: "Product2",
                ImgUri: "https://example.com/product2.png",
                Price: 299,
                Description: "This is product 2."
            );
        }

        private PagedResult<ProductDto> GetTestPagedResult(int page, int pageSize, int totalCount)
        {
            var pagedResult = new PagedResult<ProductDto>
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = GetTestProductDtos()
            };

            return pagedResult;
        }
        #endregion
    }
}
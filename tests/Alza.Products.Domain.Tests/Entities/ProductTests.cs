using Alza.Products.Domain.Entities;

namespace Alza.Products.Domain.Tests.Entities
{
    public class ProductTests
    {
        [Fact]
        public void Constructor_WithValidArguments_ShouldCreateProduct()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var product = new Product
                (
                    id,
                    "Laptop",
                    "https://example.com/laptop.png",
                    1999.99m,
                    "Gaming laptop"
                );

            // Assert
            Assert.Equal(id, product.Id);
            Assert.Equal("Laptop", product.Name);
            Assert.Equal("https://example.com/laptop.png", product.ImgUri);
            Assert.Equal(1999.99m, product.Price);
            Assert.Equal("Gaming laptop", product.Description);
        }

        [Fact]
        public void Constructor_WithNullDescription_ShouldAllowNull()
        {
            // Act
            var product = new Product
                (
                    Guid.NewGuid(),
                    "Phone",
                    "https://example.com/phone.png",
                    999.99m,
                    null
                );

            // Assert
            Assert.Null(product.Description);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_WithInvalidName_ShouldThrowArgumentNullException(string? invalidName)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new Product
                (
                    Guid.NewGuid(),
                    invalidName!,
                    "https://example.com/img.png",
                    100)
                );

            Assert.Contains("Product name is required", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_WithInvalidImgUri_ShouldThrowArgumentNullException(string? invalidImgUri)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new Product
                (
                    Guid.NewGuid(),
                    "Valid name",
                    invalidImgUri!,
                    100
                ));

            Assert.Contains("Product image URI is required", exception.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Constructor_WithNonPositivePrice_ShouldThrowArgumentOutOfRangeException(decimal invalidPrice)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
                new Product
                (
                    Guid.NewGuid(),
                    "Valid name",
                    "https://example.com/img.png",
                    invalidPrice
                ));

            Assert.Contains("Product price must be greater than zero", exception.Message);
        }

        [Fact]
        public void UpdateDescription_WithValidValue_ShouldUpdateDescription()
        {
            // Arrange
            var product = CreateValidProduct();

            // Act
            product.UpdateDescription("Updated description");

            // Assert
            Assert.Equal("Updated description", product.Description);
        }

        [Fact]
        public void UpdateDescription_WithNull_ShouldSetDescriptionToNull()
        {
            // Arrange
            var product = CreateValidProduct(description: "Old description");

            // Act
            product.UpdateDescription(null);

            // Assert
            Assert.Null(product.Description);
        }

        private static Product CreateValidProduct(
            Guid? id = null, 
            string? name = "Test Product", 
            string? imgUri = "https://example.com/img.png", 
            decimal price = 100, 
            string? description = "Initial description")
        {
            return new Product
                (
                    id ?? Guid.NewGuid(),
                    name!,
                    imgUri!,
                    price,
                    description
                );
        }
    }
}
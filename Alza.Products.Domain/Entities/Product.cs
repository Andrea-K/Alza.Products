namespace Alza.Products.Domain.Entities
{
    public class Product
    {
        private Product() { }

        public Product(Guid id, string name, string imgUri, decimal price, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Product name is required.");

            if (string.IsNullOrWhiteSpace(imgUri))
                throw new ArgumentNullException("Product image URI is required.");

            if (price <= 0)
                throw new ArgumentOutOfRangeException("Product price must be greater than zero.");

            Id = id;
            Name = name;
            ImgUri = imgUri;
            Price = price;
            Description = description;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string ImgUri { get; private set; } = null!;
        public decimal Price { get; private set; }
        public string? Description { get; private set; }

        public void UpdateDescription(string? description)
        {
            Description = description;
        }
    }
}

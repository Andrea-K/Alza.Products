namespace Alza.Products.Testing.Common.Models
{
    public sealed class TestProductSeedModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImgUri { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}

using Alza.Products.Domain.Entities;

namespace Alza.Products.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(Guid id);
        Task UpdateProductDescriptionAsync(Product product, string description);
    }
}
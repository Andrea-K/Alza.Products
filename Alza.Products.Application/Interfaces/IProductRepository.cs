using Alza.Products.Domain.Entities;

namespace Alza.Products.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetAllProductsPagedAsync(int page, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<Product?> GetProductByIdAsync(Guid id);
        Task UpdateProductDescriptionAsync(Product product, string description);
    }
}
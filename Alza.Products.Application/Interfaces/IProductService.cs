using Alza.Products.Application.DTOs;

namespace Alza.Products.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(Guid id);
        Task UpdateProductDescriptionAsync(Guid id, string description);
    }
}
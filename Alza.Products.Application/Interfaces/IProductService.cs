using Alza.Products.Application.DTOs;

namespace Alza.Products.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<PagedResult<ProductDto>> GetAllProductsPagedAsync(int page, int pageSize);
        Task<ProductDto?> GetProductByIdAsync(Guid id);
        Task UpdateProductDescriptionAsync(Guid id, string description);
    }
}
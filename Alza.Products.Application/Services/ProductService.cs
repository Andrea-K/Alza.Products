using Alza.Products.Application.DTOs;
using Alza.Products.Application.Exceptions;
using Alza.Products.Application.Interfaces;
using Alza.Products.Application.Mappings;
using Alza.Products.Domain.Entities;

namespace Alza.Products.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _repository.GetAllProductsAsync();

            return products.Select(ProductMappings.MapToDto);
        }

        public async Task<PagedResult<ProductDto>> GetAllProductsPagedAsync(int page, int pageSize)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 || pageSize > 100 ? 10 : pageSize;

            var items = await _repository.GetAllProductsPagedAsync(page, pageSize);
            var total = await _repository.GetTotalCountAsync();

            return new PagedResult<ProductDto>
            {
                Items = items.Select(ProductMappings.MapToDto).ToList(),
                Page = page,
                PageSize = pageSize,
                TotalCount = total
            };
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new EntityNotFoundException(nameof(Product), id);
            }

            return ProductMappings.MapToDto(product);
        }

        public async Task UpdateProductDescriptionAsync(Guid id, string description)
        {
            var product = await _repository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new EntityNotFoundException(nameof(Product), id);
            }

            await _repository.UpdateProductDescriptionAsync(product, description);
        }
    }
}

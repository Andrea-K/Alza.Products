using Alza.Products.Application.Interfaces;
using Alza.Products.Domain.Entities;
using Alza.Products.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Alza.Products.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductRepository(ProductDbContext dbContext, ILogger<ProductRepository> logger)
        {
            _dbContext = dbContext;
        }

        // v1
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _dbContext.Products
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync();

            return products;
        }

        // TODO: add GetAllProductsAsync v2

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            var product = await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return null;

            return product;
        }

        public async Task UpdateProductDescriptionAsync(Product product, string description)
        {
            product.UpdateDescription(description);
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}

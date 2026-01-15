using Alza.Products.Application.Interfaces;
using Alza.Products.Domain.Entities;
using Alza.Products.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Alza.Products.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _dbContext.Products
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetAllProductsPagedAsync(int page, int pageSize)
        {
            var products = await _dbContext.Products
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return products;
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _dbContext.Products.CountAsync();
        }

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

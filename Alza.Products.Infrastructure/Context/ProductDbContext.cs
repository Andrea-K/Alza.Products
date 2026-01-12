using Alza.Products.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alza.Products.Infrastructure.Context
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
    }
}

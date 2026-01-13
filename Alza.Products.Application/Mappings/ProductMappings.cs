using Alza.Products.Application.DTOs;
using Alza.Products.Domain.Entities;

namespace Alza.Products.Application.Mappings
{
    public static class ProductMappings
    {
        public static ProductDto MapToDto(Product product)
        {
            return new ProductDto(

                product.Id,
                product.Name,
                product.ImgUri,
                product.Price,
                product.Description ?? string.Empty
           );
        }
    }
}

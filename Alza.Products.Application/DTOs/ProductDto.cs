namespace Alza.Products.Application.DTOs
{
    public record ProductDto(Guid Id, string Name, string ImgUri, decimal Price, string Description);
}

using System.ComponentModel.DataAnnotations;

namespace Alza.Products.Application.DTOs
{
    public class UpdateProductDescriptionDto
    {
        [Required] // -> 400 Bad Request
        [MinLength(1)]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
    }
}

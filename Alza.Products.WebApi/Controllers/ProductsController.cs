using Alza.Products.Application.DTOs;
using Alza.Products.Application.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Alza.Products.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/products")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)] // global err handeling
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns all products without pagination (api v1)
        /// </summary>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductsAsync()
        {
            var products = await _service.GetAllProductsAsync();

            return Ok(products); // 200
        }

        /// <summary>
        /// Returns paginated list of products (default page size = 10).
        /// </summary>
        [HttpGet]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(typeof(PagedResult<ProductDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _service.GetAllProductsPagedAsync(page, pageSize);

            return Ok(products); // 200
        }

        /// <summary>
        /// Returns product by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // global err handeling
        public async Task<ActionResult<ProductDto>> GetProductByIdAsync(Guid id)
        {
            var product = await _service.GetProductByIdAsync(id);

            return Ok(product); // 200
        }

        /// <summary>
        /// Updates product description
        /// </summary>
        [HttpPatch("{id}/description")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // dto annotaion
        [ProducesResponseType(StatusCodes.Status404NotFound)] // global err handeling
        public async Task<ActionResult<ProductDto>> UpdateProductDescriptionAsync(Guid id, [FromBody] UpdateProductDescriptionDto dto)
        {
            await _service.UpdateProductDescriptionAsync(id, dto.Description);

            return NoContent(); // 204
        }
    }
}

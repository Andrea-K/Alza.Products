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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves a complete list of products.
        /// </summary>
        /// <remarks>
        /// This endpoint is available in API v1.
        /// </remarks>
        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductsAsync()
        {
            var products = await _service.GetAllProductsAsync();

            return Ok(products);
        }

        /// <summary>
        /// Retrieves a paginated list of products.
        /// </summary>
        /// <remarks>
        /// This endpoint is available in API v2.
        /// </remarks>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        [HttpGet]
        [MapToApiVersion("2.0")]
        [ProducesResponseType(typeof(PagedResult<ProductDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductsPagedAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _service.GetAllProductsPagedAsync(page, pageSize);

            return Ok(products);
        }

        /// <summary>
        /// Retrieves a single product by its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the product.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProductByIdAsync(Guid id)
        {
            var product = await _service.GetProductByIdAsync(id);
            return Ok(product);
        }

        /// <summary>
        /// Updates the description of an existing product.
        /// </summary>
        /// <remarks>
        /// This endpoint performs a partial update.
        /// Only the product description can be modified.
        /// </remarks>
        /// <param name="id">Unique identifier of the product.</param>
        [HttpPatch("{id}/description")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateProductDescriptionAsync(Guid id, [FromBody] UpdateProductDescriptionDto dto)
        {
            await _service.UpdateProductDescriptionAsync(id, dto.Description);

            return NoContent();
        }
    }
}

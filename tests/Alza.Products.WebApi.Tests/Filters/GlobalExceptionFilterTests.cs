using Alza.Products.Application.Exceptions;
using Alza.Products.WebApi.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Alza.Products.WebApi.Tests.Middleware
{
    public class GlobalExceptionFilterTests
    {
        [Fact]
        public void OnException_ShouldSetCorrectStatusCodeForNotFoundException()
        {
            // Arrange
            var productId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var exception = new EntityNotFoundException("Product", productId);
            var httpContext = new DefaultHttpContext();
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>()){ Exception = exception };
            var filter = new GlobalExceptionFilter();

            // Act
            filter.OnException(exceptionContext);

            // Assert
            var objectResult = exceptionContext.Result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(StatusCodes.Status404NotFound, objectResult.StatusCode);
        }
    }
}

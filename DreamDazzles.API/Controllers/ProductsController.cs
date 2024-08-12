
using Asp.Versioning;
using DreamDazzle.Model.Data;
using DreamDazzles.Service.Interface.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace DreamDazzles.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : BaseController<ProductsController>
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService, Serilog.ILogger slogger) : base(slogger)
        {
            _productService = productService;
        }

        [HttpGet("GetProductById/{id}")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetProductById(int id, CancellationToken token = default)
        {

            string methodName = "GetProduct";
            string httpMethod = HttpContext.Request.Method;
            string traceId  = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { PropertyId = id } as object, methodName, httpMethod, traceId, token);

            try
            {
                _logger.Information($"{methodName} - {httpMethod} Entered | trace: " + traceId);
                
                objresp = await _productService.GetProductById(id, traceId, token);
                _logger.Information($"{methodName} - {httpMethod} Exit | trace: " + traceId);

                return returnAction(objresp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }
        [HttpGet("GetAllProducts")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAllProducts(CancellationToken token = default)
        {
            string methodName = "GetProduct";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { } as object, methodName, httpMethod, traceId, token);

            try
            {
                _logger.Information($"{methodName} - {httpMethod} Entered | trace: " + traceId);

                objresp = await _productService.GetAllProducts(traceId, token);

                _logger.Information($"{methodName} - {httpMethod} Exit | trace: " + traceId);

                return returnAction(objresp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }
    }
}

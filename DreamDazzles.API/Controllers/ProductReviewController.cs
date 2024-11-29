using Asp.Versioning;
using DreamDazzle.Model.Data;
using DreamDazzles.DTO;
using DreamDazzles.DTO.Product;
using DreamDazzles.Service.Interface.Category;
using DreamDazzles.Service.Interface.Product;
using DreamDazzles.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace DreamDazzles.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductReviewController : BaseController<ProductReviewController>
    {
        private readonly IProductReviewService _productReviewService;

        public ProductReviewController(IProductReviewService productReviewService, Serilog.ILogger slogger) : base(slogger)
        {
            _productReviewService = productReviewService;
        }

        [HttpPost("AddProductCategory")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> AddProductCategory(ProductReviewDTO productReviewDTO, CancellationToken token = default)
        {
            #region asdd
            string methodName = "AddeditProductCategory";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            #endregion
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { } as object, methodName, httpMethod, traceId, token);
            try
            {
                objresp = await _productReviewService.AddProductReview(productReviewDTO, traceId, token);

                return Ok(objresp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }
        [HttpPost("GetAll")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> GetAll(CancellationToken token = default)
        {
            string methodName = "GetAllCategory";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { } as object, methodName, httpMethod, traceId, token);

            try
            {
                _logger.Information($"{methodName} - {httpMethod} Entered | trace: " + traceId);

                objresp = await _productReviewService.GetAll(traceId, token);

                _logger.Information($"{methodName} - {httpMethod} Exit | trace: " + traceId);

                return Ok(objresp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }

        [HttpGet("GetReviewById/{ProductReviewId}")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> GetReviewById(Guid ProductReviewId, CancellationToken token = default)
        {

            string methodName = "GetCategoryById";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { productReviewId = ProductReviewId } as object, methodName, httpMethod, traceId, token);

            try
            {
                _logger.Information($"{methodName} - {httpMethod} Entered | trace: " + traceId);

                objresp = await _productReviewService.GetReviewById(ProductReviewId, traceId, token);
                _logger.Information($"{methodName} - {httpMethod} Exit | trace: " + traceId);

                return returnAction(objresp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }

        [HttpGet("GetReviewByProductId/{ProductId}")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> GetReviewByProductId(Guid ProductId, CancellationToken token = default)
        {

            string methodName = "GetCategoryById";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { productId = ProductId } as object, methodName, httpMethod, traceId, token);

            try
            {
                _logger.Information($"{methodName} - {httpMethod} Entered | trace: " + traceId);

                objresp = await _productReviewService.GetReviewByProductId(ProductId, traceId, token);
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

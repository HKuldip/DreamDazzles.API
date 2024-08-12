
using Asp.Versioning;
using DreamDazzle.Model.Data;
using DreamDazzles.Service.Interface.Product;
using Microsoft.AspNetCore.Mvc;

namespace DreamDazzles.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id, CancellationToken token = default)
        {

            string methodname = "GetProduct";
            string httpmethod = HttpContext.Request.Method;
            string traceId  = HttpContext.TraceIdentifier;

            ClientResponse objresp = new()
            {
                HttpRequest = { },
                Severity = SeverityType.status,
                MinorCode = CodeMinorValueType.unauthorizedrequest
            };

            objresp = await _productService.GetProductById(id,traceId,token);
            
            return Ok(objresp);
         
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            ClientResponse objresp = new()
            {
                HttpRequest = { },
                Severity = SeverityType.status,
                MinorCode = CodeMinorValueType.unauthorizedrequest
            };

            objresp = await _productService.GetAllProducts();

            return Ok(objresp);
        }
    }
}

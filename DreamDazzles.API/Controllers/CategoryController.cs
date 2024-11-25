using Asp.Versioning;
using DreamDazzle.Model.Data;
using DreamDazzle.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DreamDazzles.DTO;
using Newtonsoft.Json.Linq;
using DreamDazzles.Service.Interface.Product;
using DreamDazzles.Service.Interface.Category;
using DreamDazzles.Service.Service;
using DreamDazzles.DTO.User;
using Microsoft.AspNetCore.Authorization;

namespace DreamDazzles.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoryController : BaseController<CategoryController>
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService, Serilog.ILogger slogger) : base(slogger)
        {
            _categoryService = categoryService;
        }

        [HttpPost("AddProductCategory")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
    
        public async Task<IActionResult> AddProductCategory(ProductCategoryDTO productCategoryDTO, CancellationToken token = default)
        {
            #region asdd
            string methodName = "AddeditProductCategory";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            #endregion
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { } as object, methodName, httpMethod, traceId, token);
            try
            {
                objresp = await _categoryService.AddProductCategory(productCategoryDTO, traceId, token);

                return Ok(objresp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }


        [HttpPost("DeleteProductCategory")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> DeleteProductCategory(Guid ProductCategoryId, CancellationToken token = default)
        {
            #region asdd
            string methodName = "DeleteProductCategory";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            #endregion
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { } as object, methodName, httpMethod, traceId, token);
            try
            {
                objresp = await _categoryService.DeleteProductCategory(ProductCategoryId, traceId, token);

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

                objresp = await _categoryService.GetAll(traceId, token);

                _logger.Information($"{methodName} - {httpMethod} Exit | trace: " + traceId);

                return Ok(objresp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }

        [HttpGet("GetCategoryById/ProductCategoryId")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> GetCategoryById(Guid ProductCategoryId, CancellationToken token = default)
        {

            string methodName = "GetCategoryById";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { productCategoryId = ProductCategoryId } as object, methodName, httpMethod, traceId, token);

            try
            {
                _logger.Information($"{methodName} - {httpMethod} Entered | trace: " + traceId);

                objresp = await _categoryService.GetCategoryById(ProductCategoryId, traceId, token);
                _logger.Information($"{methodName} - {httpMethod} Exit | trace: " + traceId);

                return returnAction(objresp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }




        [HttpPost("AddSubCategory")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> AddSubCategory(SubCategoryDTO subCategory, CancellationToken token = default)
        {
            #region asdd
            string methodName = "AddeditSubCategory";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            #endregion
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { } as object, methodName, httpMethod, traceId, token);
            try
            {
                objresp = await _categoryService.AddSubCategory(subCategory, traceId, token);

                return Ok(objresp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }

        [HttpPost("DeletesubCategory")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> DeleteSubCategory(Guid SubCategoryId, CancellationToken token = default)
        {
            #region asdd
            string methodName = "DeletesubCategory";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            #endregion
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { } as object, methodName, httpMethod, traceId, token);
            try
            {
                objresp = await _categoryService.DeleteSubCategory(SubCategoryId, traceId, token);

                return Ok(objresp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }

        [HttpPost("GetAllSubCategory")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> GetAllSubCategory(CancellationToken token = default)
        {
            string methodName = "GetAllSubCategory";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { } as object, methodName, httpMethod, traceId, token);

            try
            {
                _logger.Information($"{methodName} - {httpMethod} Entered | trace: " + traceId);

                objresp = await _categoryService.GetAllSubCategory(traceId, token);

                _logger.Information($"{methodName} - {httpMethod} Exit | trace: " + traceId);

                return Ok(objresp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }

        [HttpGet("GetSubCategoryById/SubCategoryId")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> GetSubCategoryById(Guid SubCategoryId, CancellationToken token = default)
        {

            string methodName = "GetSubCategoryById";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { subCategoryId = SubCategoryId } as object, methodName, httpMethod, traceId, token);

            try
            {
                _logger.Information($"{methodName} - {httpMethod} Entered | trace: " + traceId);

                objresp = await _categoryService.GetSubCategoryById(SubCategoryId, traceId, token);
                _logger.Information($"{methodName} - {httpMethod} Exit | trace: " + traceId);

                return returnAction(objresp);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"EXCEPTION: {methodName} - {httpMethod} => API ERROR {HttpContext.Request.Path + HttpContext.Request.QueryString} | trace: " + traceId);
                return StatusCode(StatusCodes.Status500InternalServerError, $" Failed {methodName} - {httpMethod}");
            }
        }
        [HttpGet("GetSubCategoryById/ParentsCategory")]
        [ApiVersion("1.0", Deprecated = true)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> GetSubCategorylistByParentId(Guid ParentsCategory, CancellationToken token = default)
        {

            string methodName = "GetSubCategoryById";
            string httpMethod = HttpContext.Request.Method;
            string traceId = HttpContext.TraceIdentifier;
            ClientResponse objresp = await AuthorizedLogRequestAsync(new { parentsCategory = ParentsCategory } as object, methodName, httpMethod, traceId, token);

            try
            {
                _logger.Information($"{methodName} - {httpMethod} Entered | trace: " + traceId);

                objresp = await _categoryService.GetSubCategorylistByParentId(ParentsCategory, traceId, token);
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

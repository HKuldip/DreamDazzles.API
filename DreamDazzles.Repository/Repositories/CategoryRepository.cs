using AutoMapper;
using Castle.Core.Logging;
using DreamDazzle.DTO;
using DreamDazzle.Model;
using DreamDazzle.Model.Data;
using DreamDazzle.Repository.Repositories;
using DreamDazzles.DTO;
using DreamDazzles.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Repository.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MainDBContext _context;
        private readonly ILogger<CategoryRepository> _logger;
        private readonly IMapper _mapper;


        public CategoryRepository(MainDBContext context, ILogger<CategoryRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;

        }
    
        public async Task<ClientResponse> AddProductCategory(ProductCategoryDTO productCategoryDTO, string traceid, CancellationToken token = default)
        {
            ClientResponse<ProductCategoryDTO> response = new();
            response.IsSuccess = false;
            string mname = "AddeditProductCategory";
            if (!token.IsCancellationRequested)
            {
                _logger.LogInformation($"{productCategoryDTO.ProductCategoryId}: Entered | trace: " + traceid);
                try
                {
                    if (productCategoryDTO.Action == ActionEnum.Insert)
                    {
                        var model = _mapper.Map<ProductCategory>(productCategoryDTO);
                        await _context.ProductCategories.AddAsync(model);
                    }

                    if (productCategoryDTO.Action == ActionEnum.Update)
                    {
                        var existing = await _context.ProductCategories
                            .FirstOrDefaultAsync(x => x.ProductCategoryId == productCategoryDTO.ProductCategoryId);

                        if (existing != null)
                        {
                            existing.ProductCategoryName = productCategoryDTO.ProductCategoryName;
                            existing.Description = productCategoryDTO.Description;
                            existing.IsDelete = productCategoryDTO.IsDelete;
                            existing.CategoryImage = productCategoryDTO.CategoryImage;


                            _context.ProductCategories.Update(existing);
                        }
                    }


                    var res = await _context.SaveChangesAsync();

                    if (res == 0)
                    {
                        response.Message = $"{AppConstant.NoRecords}";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                    response.HttpResponse = res;
                    response.IsSuccess = true;
                    response.Severity = SeverityType.status;

                    _logger.LogInformation($"{productCategoryDTO.ProductCategoryId}: Exit | trace: " + traceid);
                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while adding a product category.");
                     response.Message = ex.Message;

                }
            }
            if (token.IsCancellationRequested)
            {
                _logger.LogInformation($"{mname}: Request has cancelled.. | trace: " + traceid);
                response.Message = $"{mname}: Request has cancelled.. | trace: " + traceid;
            }
            return response;
        }
      
        public async Task<ClientResponse> DeleteProductCategory(Guid productCategoryId,string traceId, CancellationToken token = default)
    {
        ClientResponse response = new();
        response.IsSuccess = false;
        string methodName = "DeleteProductCategory";

        if (!token.IsCancellationRequested)
        {
            _logger.LogInformation($"{productCategoryId}: Entered | trace: {traceId}");

            try
            {
                var existingCategory = await _context.ProductCategories
                    .FirstOrDefaultAsync(x => x.ProductCategoryId == productCategoryId);
                    existingCategory.IsDelete=false;

                if (existingCategory == null)
                {
                    response.Message = "Product category not found.";
                    response.StatusCode = HttpStatusCode.NotFound;
                    _logger.LogWarning($"{productCategoryId}: Product category not found. | trace: {traceId}");
                    return response;
                }

                _context.ProductCategories.Remove(existingCategory);

                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    response.Message = "No records were affected.";
                    response.StatusCode = HttpStatusCode.NoContent;
                }
                else
                {
                    response.Message = "Product category deleted successfully.";
                    response.StatusCode = HttpStatusCode.OK;
                    response.IsSuccess = true;
                    response.HttpResponse = result;
                }

                _logger.LogInformation($"{productCategoryId}: Exit | trace: {traceId}");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the product category.");
                response.Message = "An error occurred while processing your request.";
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
        }

        if (token.IsCancellationRequested)
        {
            _logger.LogInformation($"{methodName}: Request has been cancelled. | trace: {traceId}");
            response.Message = $"{methodName}: Request has been cancelled. | trace: {traceId}";
            response.StatusCode = HttpStatusCode.RequestTimeout;
        }

        return response;
    }

        public async Task<ClientResponse> GetAll(string traceid, CancellationToken token = default)
        {
            ClientResponse<List<ProductCategoryDTO>> response = new();
            string mname = "GetAllCategory";
            response.IsSuccess = false;
            response.HttpRequest = "";
            if (!token.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"{mname}: Entered | trace: " + traceid);
                    var pro = await _context.ProductCategories.ToListAsync();


                    if (pro != null && pro.Count > 0)
                    {
                        response.StatusCode = HttpStatusCode.OK;
                        response.HttpResponse = pro;
                        response.Severity = SeverityType.status;
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = $"{AppConstant.NoRecords}";
                        response.StatusCode = HttpStatusCode.NoContent;
                        response.Severity = SeverityType.warning;
                    }

                    _logger.LogInformation($"{mname}: Exit | trace: " + traceid);
                }
                catch (Exception ex)
                {

                    response.Message = ex.Message;
                }
            }
            if (token.IsCancellationRequested)
            {
                _logger.LogInformation($"{mname}: Request has cancelled.. | trace: " + traceid);
                response.Message = $"{mname}: Request has cancelled.. | trace: " + traceid;
            }
            return response;
        }

        public async Task<ClientResponse> GetCategoryById(Guid productCategoryId, string traceid, CancellationToken token = default)
        {
            ClientResponse<ProductCategoryDTO> response = new();
            string mname = "GetCategoryById";
            response.IsSuccess = false;
            response.HttpRequest = "";
            if (!token.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"{mname}: Entered | trace: " + traceid);
                    var pro = await _context.ProductCategories.FirstOrDefaultAsync(x => x.ProductCategoryId == productCategoryId);

                    if (pro != null)
                    {
                        ProductCategoryDTO res = new ProductCategoryDTO();

                        res.ProductCategoryId = productCategoryId;
                        res.ProductCategoryName = pro.ProductCategoryName;
                        res.Description = pro.Description;
                        res.CategoryImage = pro.CategoryImage;
                        res.CreatedBy = pro.CreatedBy;
                        res.CreatedDate = pro.CreatedDate;


                        response.StatusCode = HttpStatusCode.OK;
                        response.HttpResponse = res;
                        response.Severity = SeverityType.status;
                        response.IsSuccess = true;

                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = $"{AppConstant.NoRecords}";
                        response.StatusCode = HttpStatusCode.NoContent;
                        response.Severity = SeverityType.warning;

                        _logger.LogInformation($"{mname}: {response.Message} | trace: " + traceid);
                    }
                    _logger.LogInformation($"{mname}: Exit | trace: " + traceid);

                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{mname}: Error => {ex.Message} | trace: " + traceid);
                }
            }
            if (token.IsCancellationRequested)
            {
                _logger.LogInformation($"{mname}: Request has cancelled.. | trace: " + traceid);
                response.Message = $"{mname}: Request has cancelled.. | trace: " + traceid;
            }
            return response;
        }

        public async Task<ClientResponse> AddSubCategory(SubCategoryDTO subCategory, string traceid, CancellationToken token = default)
        {
            ClientResponse<SubCategoryDTO> response = new();
            response.IsSuccess = false;
            string mname = "AddeditProductCategory";

            if (!token.IsCancellationRequested)
            {
                _logger.LogInformation($"{subCategory.SubCategoryId}: Entered | trace: " + traceid);
                try
                {
                    if (subCategory.Action == ActionEnum.Insert)
                    {
                        var parentCategory = await _context.ProductCategories.FindAsync(subCategory.ParentsCategory);
                        if (parentCategory == null)
                        {
                            response.Message = "The specified ParentId does not exist in the ProductCategories table.";
                             response.StatusCode = HttpStatusCode.BadRequest;
                             response.Severity = SeverityType.error;
                              return response;
                        }


                        var newSubCategory = _mapper.Map<SubCategory>(subCategory);
                            newSubCategory.SubCategoryId = Guid.NewGuid();
                        newSubCategory.IsDelete= false;
                        var model = _mapper.Map<SubCategory>(subCategory);
                        await _context.ProductSubCategories.AddAsync(model);
                    }


                    if (subCategory.Action == ActionEnum.Update)
                    {
                        var parentCategory = await _context.ProductCategories.FindAsync(subCategory.ParentsCategory);
                        if (parentCategory == null)
                        {
                            response.Message = "The specified ParentId does not exist in the ProductCategories table.";
                            response.StatusCode = HttpStatusCode.BadRequest;
                            response.Severity = SeverityType.error;
                            return response;
                        }


                        var newSubCategory = _mapper.Map<SubCategory>(subCategory);
                        newSubCategory.SubCategoryId = Guid.NewGuid();
                        newSubCategory.IsDelete = false;
                        var model = _mapper.Map<SubCategory>(subCategory);
                        var existing = await _context.ProductSubCategories
                            .FirstOrDefaultAsync(x => x.SubCategoryId == subCategory.SubCategoryId);

                        if (existing != null)
                        {
                            existing.SubCategoryName = subCategory.SubCategoryName;
                            existing.Description = subCategory.Description;
                            existing.IsDelete = subCategory.IsDelete;
                            existing.SubCategoryImage = subCategory.SubCategoryImage;

                            _context.ProductSubCategories.Update(existing);
                        }
                    }


                    var res = await _context.SaveChangesAsync();

                    if (res == 0)
                    {
                        response.Message = $"{AppConstant.NoRecords}";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }

                    response.HttpResponse = res;
                    response.IsSuccess = true;
                    response.Severity = SeverityType.status;

                    _logger.LogInformation($"{subCategory.SubCategoryId}: Successfully processed | trace: " + traceid);
                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while adding a product subcategory.");
                    response.Message = ex.Message;
                }
            }

            if (token.IsCancellationRequested)
            {
                _logger.LogInformation($"{mname}: Request has cancelled.. | trace: " + traceid);
                response.Message = $"{mname}: Request has cancelled.. | trace: " + traceid;
            }

            return response;
        }


        public async Task<ClientResponse> DeleteSubCategory(Guid SubCategoryId, string traceId, CancellationToken token = default)
        {
            ClientResponse response = new();
            response.IsSuccess = false;
            string methodName = "DeleteProductCategory";

            if (!token.IsCancellationRequested)
            {
                _logger.LogInformation($"{SubCategoryId}: Entered | trace: {traceId}");

                try
                {
                    var existingCategory = await _context.ProductSubCategories
                        .FirstOrDefaultAsync(x => x.SubCategoryId == SubCategoryId);

                    if (existingCategory == null)
                    {
                        response.Message = "Product category not found.";
                        response.StatusCode = HttpStatusCode.NotFound;
                        _logger.LogWarning($"{SubCategoryId}: Product category not found. | trace: {traceId}");
                        return response;
                    }

                    _context.ProductSubCategories.Remove(existingCategory);

                    var result = await _context.SaveChangesAsync();

                    if (result == 0)
                    {
                        response.Message = "No records were affected.";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                    else
                    {
                        response.Message = "Product category deleted successfully.";
                        response.StatusCode = HttpStatusCode.OK;
                        response.IsSuccess = true;
                        response.HttpResponse = result;
                    }

                    _logger.LogInformation($"{SubCategoryId}: Exit | trace: {traceId}");
                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting the product category.");
                    response.Message = "An error occurred while processing your request.";
                    response.StatusCode = HttpStatusCode.InternalServerError;
                }
            }

            if (token.IsCancellationRequested)
            {
                _logger.LogInformation($"{methodName}: Request has been cancelled. | trace: {traceId}");
                response.Message = $"{methodName}: Request has been cancelled. | trace: {traceId}";
                response.StatusCode = HttpStatusCode.RequestTimeout;
            }

            return response;
        }

        public async Task<ClientResponse> GetAllSubCategory(string traceid, CancellationToken token = default)
        {
            ClientResponse<List<SubCategoryDTO>> response = new();
            string mname = "GetAllSubCategory";
            response.IsSuccess = false;
            response.HttpRequest = "";
            if (!token.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"{mname}: Entered | trace: " + traceid);
                    var pro = await _context.ProductSubCategories.ToListAsync();


                    if (pro != null && pro.Count > 0)
                    {
                        response.StatusCode = HttpStatusCode.OK;
                        response.HttpResponse = pro;
                        response.Severity = SeverityType.status;
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = $"{AppConstant.NoRecords}";
                        response.StatusCode = HttpStatusCode.NoContent;
                        response.Severity = SeverityType.warning;
                    }

                    _logger.LogInformation($"{mname}: Exit | trace: " + traceid);
                }
                catch (Exception ex)
                {

                    response.Message = ex.Message;
                }
            }
            if (token.IsCancellationRequested)
            {
                _logger.LogInformation($"{mname}: Request has cancelled.. | trace: " + traceid);
                response.Message = $"{mname}: Request has cancelled.. | trace: " + traceid;
            }
            return response;
        }

        public async Task<ClientResponse> GetSubCategoryById(Guid SubCategoryId, string traceid, CancellationToken token = default)
        {
            ClientResponse<SubCategoryDTO> response = new();
            string mname = "GetSubCategoryById";
            response.IsSuccess = false;
            response.HttpRequest = "";
            if (!token.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"{mname}: Entered | trace: " + traceid);
                    var pro = await _context.ProductSubCategories.FirstOrDefaultAsync(x => x.SubCategoryId == SubCategoryId);

                    if (pro != null)
                    {
                        SubCategoryDTO res = new SubCategoryDTO();

                        res.SubCategoryId = SubCategoryId;
                        res.SubCategoryName = pro.SubCategoryName;
                        res.Description = pro.Description;
                        res.ParentsCategory = pro.ParentsCategory;
                        res.SubCategoryImage = pro.SubCategoryImage;
                        res.CreatedBy = pro.CreatedBy;
                        res.CreatedDate=pro.CreatedDate;


                        response.StatusCode = HttpStatusCode.OK;
                        response.HttpResponse = res;
                        response.Severity = SeverityType.status;
                        response.IsSuccess = true;

                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = $"{AppConstant.NoRecords}";
                        response.StatusCode = HttpStatusCode.NoContent;
                        response.Severity = SeverityType.warning;

                        _logger.LogInformation($"{mname}: {response.Message} | trace: " + traceid);
                    }
                    _logger.LogInformation($"{mname}: Exit | trace: " + traceid);

                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{mname}: Error => {ex.Message} | trace: " + traceid);
                }
            }
            if (token.IsCancellationRequested)
            {
                _logger.LogInformation($"{mname}: Request has cancelled.. | trace: " + traceid);
                response.Message = $"{mname}: Request has cancelled.. | trace: " + traceid;
            }
            return response;
        }

        public async Task<ClientResponse> GetSubCategorylistByParentId(Guid ParentsCategory, string traceid, CancellationToken token = default)
        {
            ClientResponse<SubCategoryDTO> response = new();
            string mname = "GetSubCategoryById";
            response.IsSuccess = false;
            response.HttpRequest = "";
            if (!token.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"{mname}: Entered | trace: " + traceid);
                    var pro = await _context.ProductSubCategories.FirstOrDefaultAsync(x => x.ParentsCategory == ParentsCategory);

                    if (pro != null)
                    {
                        SubCategoryDTO res = new SubCategoryDTO();

                        res.SubCategoryId = pro.SubCategoryId;
                        res.SubCategoryName = pro.SubCategoryName;
                        res.Description = pro.Description;
                        res.ParentsCategory = pro.ParentsCategory;
                        res.SubCategoryImage = pro.SubCategoryImage;
                        res.CreatedBy = pro.CreatedBy;
                        res.CreatedDate = pro.CreatedDate;


                        response.StatusCode = HttpStatusCode.OK;
                        response.HttpResponse = res;
                        response.Severity = SeverityType.status;
                        response.IsSuccess = true;

                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = $"{AppConstant.NoRecords}";
                        response.StatusCode = HttpStatusCode.NoContent;
                        response.Severity = SeverityType.warning;

                        _logger.LogInformation($"{mname}: {response.Message} | trace: " + traceid);
                    }
                    _logger.LogInformation($"{mname}: Exit | trace: " + traceid);

                    return response;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{mname}: Error => {ex.Message} | trace: " + traceid);
                }
            }
            if (token.IsCancellationRequested)
            {
                _logger.LogInformation($"{mname}: Request has cancelled.. | trace: " + traceid);
                response.Message = $"{mname}: Request has cancelled.. | trace: " + traceid;
            }
            return response;
        }
    }
}

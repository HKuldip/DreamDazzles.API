using AutoMapper;
using DreamDazzle.Model.Data;
using DreamDazzle.Model;
using DreamDazzles.DTO;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DreamDazzles.DTO.Product;
using DreamDazzles.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DreamDazzles.Repository.Repositories
{
    public class ProductReviewRepository : IProductReviewRepository
    {
        private readonly MainDBContext _context;
        private readonly ILogger<ProductReviewRepository> _logger;
        private readonly IMapper _mapper;


        public ProductReviewRepository(MainDBContext context, ILogger<ProductReviewRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;

        }

        public async Task<ClientResponse> AddProductReview(ProductReviewDTO productReviewDTO, string traceid, CancellationToken token = default)
        {
            ClientResponse<ProductReviewDTO> response = new();
            response.IsSuccess = false;
            string mname = "AddProductReview";

            if (!token.IsCancellationRequested)
            {
                _logger.LogInformation($"{productReviewDTO.ProductReviewId}: Entered | trace: " + traceid);

                try
                {

                    if (productReviewDTO.Action == ActionEnum.Insert)
                    {
                        productReviewDTO.ProductReviewId = new();
                        var model = _mapper.Map<ProductReview>(productReviewDTO);
                        await _context.ProductReviews.AddAsync(model);
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

                    _logger.LogInformation($"{productReviewDTO.ProductReviewId}: Exit | trace: " + traceid);
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
        public async Task<ClientResponse> GetAll(string traceid, CancellationToken token = default)
        {
            ClientResponse<List<ProductReviewDTO>> response = new();
            string mname = "GetAll";
            response.IsSuccess = false;
            response.HttpRequest = "";

            if (!token.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"{mname}: Entered | trace: " + traceid);
                    var pro = await _context.ProductReviews.ToListAsync();

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
                    response.StatusCode = HttpStatusCode.InternalServerError;
                }
            }

            if (token.IsCancellationRequested)
            {
                _logger.LogInformation($"{mname}: Request has cancelled.. | trace: " + traceid);
                response.Message = $"{mname}: Request has cancelled.. | trace: " + traceid;
            }

            return response;
        }
        public async Task<ClientResponse> GetReviewById(Guid ProductReviewId, string traceid, CancellationToken token = default)
        {
            ClientResponse<ProductCategoryDTO> response = new();
            string mname = "GetReviewById";
            response.IsSuccess = false;
            response.HttpRequest = "";
            if (!token.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"{mname}: Entered | trace: " + traceid);
                    var pro = await _context.ProductReviews.FirstOrDefaultAsync(x => x.ProductReviewId == ProductReviewId);

                    if (pro != null)
                    {
                        ProductReviewDTO res = new ProductReviewDTO();

                        res.ProductReviewId = ProductReviewId;
                        res.ProductId = pro.ProductId;
                        res.Description = pro.Description;
                        res.UserId = pro.UserId;
                        res.Ratings = pro.Ratings;
                        res.ReviewDate = pro.ReviewDate;


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
        public async Task<ClientResponse> GetReviewByProductId(Guid ProductId, string traceid, CancellationToken token = default)
        {
            ClientResponse<ProductCategoryDTO> response = new();
            string mname = "GetReviewById";
            response.IsSuccess = false;
            response.HttpRequest = "";
            if (!token.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"{mname}: Entered | trace: " + traceid);
                    var pro = await _context.ProductReviews.FirstOrDefaultAsync(x => x.ProductId == ProductId);

                    if (pro != null)
                    {
                        ProductReviewDTO res = new ProductReviewDTO();

                        res.ProductReviewId = ProductId;
                        res.ProductId = pro.ProductId;
                        res.Description = pro.Description;
                        res.UserId = pro.UserId;
                        res.Ratings = pro.Ratings;
                        res.ReviewDate = pro.ReviewDate;


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

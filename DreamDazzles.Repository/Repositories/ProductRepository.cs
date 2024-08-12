
using DreamDazzle.DTO;
using DreamDazzle.Model;
using DreamDazzle.Model.Data;
using DreamDazzle.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzle.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MainDBContext _context;
        private readonly ILogger<ProductRepository> _logger;


        public ProductRepository(MainDBContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }



        public async Task<ClientResponse> GetAllProducts()
        {
            ClientResponse<List<ProductDTO>> response = new();
            response.IsSuccess = false;
            response.HttpRequest = "";

            try
            {
                var pro = await _context.Product.ToListAsync();


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


            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ClientResponse> GetProductByIdAsync(int id, string traceid, CancellationToken token = default)
        {
            ClientResponse<ProductDTO> response = new();
            string mname = "GetProductById";
            response.IsSuccess = false;
            response.HttpRequest = "";
            if (!token.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation($"{mname}: Entered | trace: " + traceid);
                    var pro = await _context.Product.FirstOrDefaultAsync(x => x.ProductID == id);

                    if (pro != null)
                    {
                        ProductDTO res = new ProductDTO();

                        res.ProductID = id;
                        res.ProductName = pro.ProductName;
                        res.ProductPrice = pro.ProductPrice;
                        res.ProductQuantity = pro.ProductQuantity;


                        response.StatusCode = HttpStatusCode.OK;
                        response.HttpResponse = res;
                        response.Severity = SeverityType.status;
                        response.IsSuccess = true;

                        //_logger.LogInformation($"{mname}: {AppConstant.SimpleSuccess}, Records Count : {PropertyDTOS.Count}| trace: " + traceid);
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

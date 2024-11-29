using DreamDazzle.Model.Data;
using DreamDazzles.DTO;
using DreamDazzles.DTO.Product;
using DreamDazzles.Repository.Interface;
using DreamDazzles.Repository.Repositories;
using DreamDazzles.Service.Interface.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Service.Service
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly IProductReviewRepository _productReviewRepository;

        public ProductReviewService(IProductReviewRepository productReviewRepository)
        {
            _productReviewRepository = productReviewRepository;
        }

        public async Task<ClientResponse> AddProductReview(ProductReviewDTO productReviewDTO, string traceid, CancellationToken token = default)
        {
            try
            {
                return await _productReviewRepository.AddProductReview(productReviewDTO, traceid, token);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<ClientResponse> GetAll(string traceid, CancellationToken token = default)
        {
            try
            {
                return await _productReviewRepository.GetAll(traceid, token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ClientResponse> GetReviewById(Guid ProductReviewId, string traceid, CancellationToken token = default)
        {
            try
            {
                return await _productReviewRepository.GetReviewById(ProductReviewId, traceid, token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ClientResponse> GetReviewByProductId(Guid ProductId, string traceid, CancellationToken token = default)
        {
            try
            {
                return await _productReviewRepository.GetReviewByProductId(ProductId, traceid, token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

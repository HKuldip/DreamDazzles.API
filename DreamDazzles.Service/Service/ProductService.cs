using DreamDazzle.DTO;
using DreamDazzle.Model.Data;
using DreamDazzle.Repository.Interface;
using DreamDazzles.Service.Interface;
using DreamDazzles.Service.Interface.Product;

namespace DreamDazzles.Service.Service
{
    public class ProductService : IProductService
    {


        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ClientResponse> GetAllProducts()
        {
            try
            {
                return await _productRepository.GetAllProducts();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ClientResponse> GetProductByIdAsync(int id, string traceid, CancellationToken token = default)
        {
            try
            {
                return await _productRepository.GetProductByIdAsync(id, traceid, token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }

}

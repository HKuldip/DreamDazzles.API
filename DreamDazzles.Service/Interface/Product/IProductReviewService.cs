using DreamDazzle.Model.Data;
using DreamDazzles.DTO;
using DreamDazzles.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Service.Interface.Product
{
    public interface IProductReviewService
    {
        Task<ClientResponse> AddProductReview(ProductReviewDTO productReviewDTO, string traceid, CancellationToken token = default);
        Task<ClientResponse> GetAll(string traceid, CancellationToken token = default);
        Task<ClientResponse> GetReviewById(Guid ProductReviewId, string traceid, CancellationToken token = default);
        Task<ClientResponse> GetReviewByProductId(Guid ProductId, string traceid, CancellationToken token = default);

    }
}

using DreamDazzle.DTO;
using DreamDazzle.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Service.Interface.Product
{
    public interface IProductService
    {
        Task<ClientResponse> GetProductByIdAsync(int id, string traceid, CancellationToken token = default);
        Task<ClientResponse> GetAllProducts();
    }
}

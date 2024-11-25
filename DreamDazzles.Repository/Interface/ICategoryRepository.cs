using DreamDazzle.Model.Data;
using DreamDazzles.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<ClientResponse> AddProductCategory(ProductCategoryDTO productCategoryDTO, string traceid, CancellationToken token = default);
        Task<ClientResponse> DeleteProductCategory(Guid ProductCategoryId, string traceid, CancellationToken token = default);


        Task<ClientResponse> AddSubCategory(SubCategoryDTO subCategory, string traceid, CancellationToken token = default);
        Task<ClientResponse> DeleteSubCategory(Guid SubCategoryId, string traceid, CancellationToken token = default);

    }
}

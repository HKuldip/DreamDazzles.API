using DreamDazzle.Model;
using DreamDazzle.Model.Data;
using DreamDazzle.Repository.Interface;
using DreamDazzles.DTO;
using DreamDazzles.DTO.User;
using DreamDazzles.Repository.Interface;
using DreamDazzles.Repository.Repositories;
using DreamDazzles.Service.Interface.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.Service.Service
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ClientResponse> AddProductCategory(ProductCategoryDTO productCategoryDTO, string traceid, CancellationToken token = default)
        {
            try
            {
                return await _categoryRepository.AddProductCategory(productCategoryDTO, traceid, token);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ClientResponse> DeleteProductCategory(Guid ProductCategoryId, string traceid, CancellationToken token = default)
        {
            try
            {
                return await _categoryRepository.DeleteProductCategory(ProductCategoryId, traceid, token);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<ClientResponse> AddSubCategory(SubCategoryDTO subCategory, string traceid, CancellationToken token = default)
        {
            try
            {
                return await _categoryRepository.AddSubCategory(subCategory, traceid, token);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ClientResponse> DeleteSubCategory(Guid SubCategoryId, string traceid, CancellationToken token = default)
        {
            try
            {
                return await _categoryRepository.DeleteSubCategory(SubCategoryId, traceid, token);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

using AutoMapper;
using DreamDazzle.DTO;
using DreamDazzle.Model;
using DreamDazzles.DTO;
using DreamDazzles.DTO.Product;

namespace DreamDazzles.API.Utility.Extension
{
    public class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<ProductCategory, ProductCategoryDTO>();
            CreateMap<ProductCategoryDTO, ProductCategory>();

            CreateMap<SubCategory, SubCategoryDTO>();
            CreateMap<SubCategoryDTO, SubCategory>();

            CreateMap<ProductReview, ProductReviewDTO>();
            CreateMap<ProductReviewDTO, ProductReview>();

            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();

        }

    }
}
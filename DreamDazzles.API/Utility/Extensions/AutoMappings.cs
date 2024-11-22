using AutoMapper;
using DreamDazzle.Model;
using DreamDazzles.DTO;

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

        }

    }
}
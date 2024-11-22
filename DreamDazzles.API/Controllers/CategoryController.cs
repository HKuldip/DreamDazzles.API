using Asp.Versioning;
using DreamDazzle.Model.Data;
using DreamDazzle.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DreamDazzles.DTO;

namespace DreamDazzles.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoryController : Controller
    {
        private readonly MainDBContext _context;
        private readonly IMapper _mapper;

        public CategoryController(IMapper mapper, MainDBContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductCategory(ProductCategoryDTO productCategory)
        {
            try
            {
                if (productCategory == null)
                    return BadRequest("Invalid product category.");

                productCategory.ProductCategoryId = Guid.NewGuid();
                productCategory.CreatedDate = DateTime.UtcNow;
                productCategory.IsDelete = false;

                var newModel = _mapper.Map<ProductCategory>(productCategory);
                await _context.ProductCategories.AddAsync(newModel);
                await _context.SaveChangesAsync();

                var resultDto = _mapper.Map<ProductCategoryDTO>(newModel);
                return Ok(resultDto);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                return StatusCode(500, "An error occurred while saving the product category.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProductCategory(Guid id, ProductCategoryDTO updatedCategory)
        {
            try
            {

                var existingCategory = await _context.ProductCategories.FindAsync(id);
             
                if (existingCategory == null)
                    return NotFound("Product category not found.");

                _mapper.Map(updatedCategory, existingCategory);

                await _context.SaveChangesAsync();

                var resultDto = _mapper.Map<ProductCategoryDTO>(existingCategory);
                return Ok(resultDto);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                return StatusCode(500, "An error occurred while updating the product category.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(Guid id)
        {
            try
            {
                var productCategory = await _context.ProductCategories.FindAsync(id);
                if (productCategory == null)

                    return NotFound("Product category not found.");

                _context.ProductCategories.Remove(productCategory);
                await _context.SaveChangesAsync();

                return Ok("Product category deleted");
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                return StatusCode(500, "An error occurred while deleting the product category.");
            }
        }

        [HttpPost("AddSubCategory")]
        public async Task<IActionResult> AddSubCategory([FromBody] SubCategoryDTO productSubCategoryDto)
        {
            if (productSubCategoryDto == null)
                return BadRequest("Invalid data.");

            var parentCategory = await _context.ProductCategories.FindAsync(productSubCategoryDto.ParentsCategory);
            if (parentCategory == null)
                return NotFound("Parent category not found.");

            var newSubCategory = _mapper.Map<SubCategory>(productSubCategoryDto);
            newSubCategory.SubCategoryId = Guid.NewGuid();
            newSubCategory.CreatedDate = DateTime.UtcNow;
            newSubCategory.IsDelete = false;
            _context.ProductSubCategories.Add(newSubCategory);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<SubCategoryDTO>(newSubCategory);
            return Ok(resultDto);
        }

        [HttpPut("EditSubCategory/{id}")]
        public async Task<IActionResult> EditSubCategory(Guid id, [FromBody] SubCategoryDTO updatedSubCategoryDto)
        {
            var existingSubCategory = await _context.ProductSubCategories.FindAsync(id);

            if (existingSubCategory == null)
                return NotFound("Sub-category not found.");

            if (updatedSubCategoryDto.ParentsCategory != null)
            {
                var parentCategory = await _context.ProductCategories.FindAsync(updatedSubCategoryDto.ParentsCategory);
                if (parentCategory == null)
                    return NotFound("Parent category not found.");
            }

            _mapper.Map(updatedSubCategoryDto, existingSubCategory);

            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<SubCategoryDTO>(existingSubCategory);
            return Ok(resultDto);
        }

        [HttpDelete("DeleteSubCategory/{id}")]
        public async Task<IActionResult> DeleteSubCategory(Guid id)
        {
            var subCategory = await _context.ProductSubCategories.FindAsync(id);

            if (subCategory == null)
                return NotFound("Sub-category not found.");

            _context.ProductSubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();

            return Ok("Product Sub category deleted");
        }
    }
}

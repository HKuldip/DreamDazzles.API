using Asp.Versioning;
using DreamDazzle.Model.Data;
using DreamDazzle.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DreamDazzles.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CategoryController : Controller
    {
        private readonly MainDBContext _context;
        private static List<ProductCategory> _productCategories = new List<ProductCategory>();

        public CategoryController(MainDBContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> AddProductCategory([FromBody] ProductCategory productCategory)
        {
            try
            {
                if (productCategory == null)
                    return BadRequest("Invalid product category.");
                productCategory.ProductCategoryId = Guid.NewGuid();
                productCategory.CreatedDate = DateTime.UtcNow;
                productCategory.IsDelete = false;
                _context.ProductCategories.Add(productCategory);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
            }
            return Ok(productCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProductCategory(Guid id, ProductCategory updatedCategory)
        {
            var existingCategory = await _context.ProductCategories.FindAsync(id);
            if (existingCategory == null) return NotFound("Product category not found");

            existingCategory.ProductCategoryName = updatedCategory.ProductCategoryName;
            existingCategory.Description = updatedCategory.Description;
            existingCategory.CategoryImage = updatedCategory.CategoryImage;
            existingCategory.IsDelete = updatedCategory.IsDelete;
            existingCategory.CreatedBy = updatedCategory.CreatedBy;

            await _context.SaveChangesAsync();
            return Ok(existingCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(Guid id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null) return NotFound("Product category not found");

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();
            return Ok("Product category deleted");
        }


        [HttpPost("AddSubCategory")]
        public async Task<IActionResult> AddSubCategory([FromBody] SubCategory productSubCategorie)
        {
            if (productSubCategorie == null) return BadRequest("Invalid data.");

            // Verify that the ParentsCategory exists
            var parentCategory = await _context.ProductCategories.FindAsync(productSubCategorie.ParentsCategory);
            if (parentCategory == null)
                return NotFound("Parent category not found.");

            productSubCategorie.SubCategoryId = Guid.NewGuid();
            productSubCategorie.CreatedDate = DateTime.UtcNow;

            _context.ProductSubCategories.Add(productSubCategorie);
            await _context.SaveChangesAsync();

            return Ok(productSubCategorie);
        }

        [HttpPut("EditSubCategory/{id}")]
        public async Task<IActionResult> EditSubCategory(Guid id, [FromBody] SubCategory updatedSubCategorie)
        {
            var existingSubCategory = await _context.ProductSubCategories.FindAsync(id);

            if (existingSubCategory == null) return NotFound("Sub-category not found.");

            // Verify that the new ParentsCategory exists
            if (updatedSubCategorie.ParentsCategory != null)
            {
                var parentCategory = await _context.ProductCategories.FindAsync(updatedSubCategorie.ParentsCategory);
                if (parentCategory == null)
                    return NotFound("Parent category not found.");
            }

            existingSubCategory.SubCategoryName = updatedSubCategorie.SubCategoryName;
            existingSubCategory.ParentsCategory = updatedSubCategorie.ParentsCategory;
            existingSubCategory.Description = updatedSubCategorie.Description;
            existingSubCategory.SubCategoryImage = updatedSubCategorie.SubCategoryImage;
            existingSubCategory.IsDelete = updatedSubCategorie.IsDelete;
            existingSubCategory.CreatedBy = updatedSubCategorie.CreatedBy;

            _context.ProductSubCategories.Update(existingSubCategory);
            await _context.SaveChangesAsync();

            return Ok(existingSubCategory);
        }

        [HttpDelete("DeleteSubCategory/{id}")]
        public async Task<IActionResult> DeleteSubCategory(Guid id)
        {
            var subCategory = await _context.ProductSubCategories.FindAsync(id);

            if (subCategory == null) return NotFound("Sub-category not found.");

            _context.ProductSubCategories.Remove(subCategory);
            await _context.SaveChangesAsync();

            return Ok("Product Sub category deleted");
        }
    }
}

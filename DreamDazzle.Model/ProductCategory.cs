using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDazzle.Model
{
    
    public class ProductCategory
    {
   
        public Guid ProductCategoryId { get; set; }
        public string? ProductCategoryName { get; set; }
        public string? Description { get; set; }
        public string? CategoryImage { get; set; }
        public bool IsDelete { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
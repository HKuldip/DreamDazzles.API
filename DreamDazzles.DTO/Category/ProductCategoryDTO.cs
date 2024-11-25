using DreamDazzle.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.DTO

{
    public class ProductCategoryDTO
    {
        [Key]
        public Guid ProductCategoryId { get; set; }
        public string? ProductCategoryName { get; set; }
        public string? Description { get; set; }
        public string? CategoryImage { get; set; }
        public bool IsDelete { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public ActionEnum Action { get; set; }
    }
}

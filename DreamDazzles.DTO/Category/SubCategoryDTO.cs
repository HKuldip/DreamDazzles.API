using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.DTO
{
    public class SubCategoryDTO
    {
        [Key]
        public Guid SubCategoryId { get; set; }
        public string? SubCategoryName { get; set; }
        public Guid? ParentsCategory { get; set; }
        public string? Description { get; set; }
        public string? SubCategoryImage { get; set; }
        public bool IsDelete { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

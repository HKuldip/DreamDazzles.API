using DreamDazzle.DTO;
using DreamDazzle.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.DTO.Product
{
    public class ProductReviewDTO
    {
        [Key]
        public Guid ProductReviewId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public string? Description { get; set; }
        public decimal Ratings { get; set; }
        public DateTime ReviewDate { get; set; }

        public ActionEnum Action { get; set; }
    }
}

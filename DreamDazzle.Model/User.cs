using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamDazzle.Model
{
    public class User : IdentityUser
    {
        
        public string PasswordResetToken;

        public User() : base() { }
        public string? FirstName { get; set; }
        public string? Lastname { get; set; }
        [NotMapped]
        public object SecurityCode { get; set; }
    }
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
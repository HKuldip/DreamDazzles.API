using Microsoft.AspNetCore.Identity;

namespace DreamDazzle.Model
{
    public class User : IdentityUser
    {
        public User() : base() { }
        public string? FirstName { get; set; }
        public string? Lastname { get; set; }
 
    }
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzle.Model.User
{
    public class AspNetUsers
    {
        public bool IsDeleted { get; set; } = false;
        public string PreferredLanguage { get; set; } = "en";
    }


    public class AspNetRoles
    {
        public string Description { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    
}

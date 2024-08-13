﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamDazzles.DTO.User
{
    public class Login
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class TokenResponse
    {
        public string? token { get; set; }
        public DateTime? Expiration { get; set; }

    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Models
{
    public class Role : IdentityRole
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
    }
}


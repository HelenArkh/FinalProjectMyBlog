using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Image { get; set; }

        public string Status { get; set; }

        public string About { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public string GetFullName()
        {
            return FirstName + " " + MiddleName + " " + LastName;
        }

        public User()
        {
            Image = "https://via.placeholder.com/500";
            Status = "Ура! Я блоггер!";
            About = "Информация обо мне.";
        }
    }
}


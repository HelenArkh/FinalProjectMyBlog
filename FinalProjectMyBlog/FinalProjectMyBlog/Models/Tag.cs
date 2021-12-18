using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Models
{
    public class Tag
    {
        public string Id { get; set; }
        public string TagName { get; set; }
        public string CurrentTagId { get; set; }

        public Tag CurrentTag { get; set; }
    }
}

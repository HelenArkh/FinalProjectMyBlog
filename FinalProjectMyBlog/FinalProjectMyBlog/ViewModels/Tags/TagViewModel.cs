using FinalProjectMyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.ViewModels.Tags
{
    public class TagViewModel
    {
        public Tag Tag { get; set; }

        public TagViewModel(/*Publication publication*/)
        {
            //Publication = publication;
        }

        public List<Tag> Tags { get; set; }
    }
}

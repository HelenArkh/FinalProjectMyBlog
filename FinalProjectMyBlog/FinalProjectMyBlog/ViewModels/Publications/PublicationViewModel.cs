using FinalProjectMyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.ViewModels.Publications
{
    public class PublicationViewModel
    {
        public Publication Publication { get; set; }

        public PublicationViewModel(/*Publication publication*/)
        {
            //Publication = publication;
        }

        public List<Publication> Publications { get; set; }
    }
}

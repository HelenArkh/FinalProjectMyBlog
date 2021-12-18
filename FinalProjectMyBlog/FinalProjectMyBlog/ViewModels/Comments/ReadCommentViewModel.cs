using FinalProjectMyBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.ViewModels.Comments
{
    public class ReadCommentViewModel
    {
        public Publication Publication { get; set; }

        public ReadCommentViewModel(Publication publication)
        {
            Publication = publication;
        }

        public List<Comment> Comments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Data.Queries
{
    public class UpdateCommentQuery
    {
        public string NewText { get; }

        public UpdateCommentQuery(string newText = null)
        {
            NewText = newText;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Data.Queries
{
    public class UpdatePublicationQuery
    {
        public string NewTitle { get; }
        public string NewText { get; }

        public UpdatePublicationQuery(string newTitle = null, string newText = null)
        {
            NewTitle = newTitle;
            NewText = newText;
        }
    }
}

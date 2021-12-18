using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Data.Queries
{
    public class UpdateTagQuery
    {
        public string NewTagName { get; }

        public UpdateTagQuery(string newTagName = null)
        {
            NewTagName = newTagName;
        }
    }
}

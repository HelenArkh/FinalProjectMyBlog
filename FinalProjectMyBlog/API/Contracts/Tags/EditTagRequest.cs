using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Contracts.Tags
{
    public class EditTagRequest
    {
        public string Id { get; set; }
        public string TagName { get; set; }
    }
}

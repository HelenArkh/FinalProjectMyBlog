using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Contracts.Tags
{
    public class GetTagsResponse
    {
        public int TagAmount { get; set; }
        public TagView[] Tags { get; set; }
    }
        public class TagView
    {
        public string Id { get; set; }
        public string TagName { get; set; }
    }   
}

using FinalProjectMyBlog.Models;
using FinalProjectMyBlog.ViewModels.Publications;
using FinalProjectMyBlog.ViewModels.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Extentions
{
    public static class TagFromModel
    {
        public static Tag Convert(this Tag tag, TagCreateViewModel tagcreatevm)
        {
            tag.TagName = tagcreatevm.TagName;

            return tag;
        }
    }
}

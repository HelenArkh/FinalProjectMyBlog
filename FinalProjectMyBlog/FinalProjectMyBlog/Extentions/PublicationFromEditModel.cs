using FinalProjectMyBlog.Models;
using FinalProjectMyBlog.ViewModels.Publications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Extentions
{
    public static class PublicationFromEditModel
    {
        public static Publication Convert(this Publication publication, PublicationEditViewModel publicationcreatevm)
        {
            publication.Title = publicationcreatevm.Title;
            publication.Text = publicationcreatevm.Text;

            return publication;
        }
    }
}

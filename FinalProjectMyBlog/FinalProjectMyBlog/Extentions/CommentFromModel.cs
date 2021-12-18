using FinalProjectMyBlog.Models;
using FinalProjectMyBlog.ViewModels.Comments;
using FinalProjectMyBlog.ViewModels.Publications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Extentions
{
    public static class CommentFromModel
    {
        public static Comment Convert(this Comment comment, CommentCreateViewModel commentcreatevm)
        {
            comment.PublicationId = commentcreatevm.PublicationId;
            comment.Text = commentcreatevm.Text;

            return comment;
        }
    }
}

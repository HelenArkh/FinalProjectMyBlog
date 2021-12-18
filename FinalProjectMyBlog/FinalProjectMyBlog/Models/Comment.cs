using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Models
{
    public class Comment
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string PublicationId { get; set; }
        public string CurrentCommentId { get; set; }

        public Comment CurrentComment { get; set; }
        public Publication Publication { get; set; }
    }
}

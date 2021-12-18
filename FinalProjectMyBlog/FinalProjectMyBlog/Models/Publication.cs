using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectMyBlog.Models
{
    public class Publication
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public string CurrentPublicationId { get; set; }

        public Publication CurrentPublication { get; set; }
        public User User { get; set; }
    }
}

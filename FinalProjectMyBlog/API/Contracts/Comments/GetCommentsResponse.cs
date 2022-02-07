using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Contracts.Tags
{
    public class GetCommentsResponse
    {
        public int CommentAmount { get; set; }
        public CommentView[] Comments { get; set; }
    }
        public class CommentView
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string PublicationId { get; set; }
    }   
}

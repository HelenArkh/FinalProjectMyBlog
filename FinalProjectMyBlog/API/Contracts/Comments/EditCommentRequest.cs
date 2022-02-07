using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Contracts.Tags
{
    public class EditCommentRequest
    {
        public string Id { get; set; }
        public string PublicationId { get; set; }
        public string Text { get; set; }
    }
}

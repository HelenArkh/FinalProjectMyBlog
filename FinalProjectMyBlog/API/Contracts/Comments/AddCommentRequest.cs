using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Contracts.Tags
{
    /// <summary>
    /// Добавляет новый комментарий.
    /// </summary>
    public class AddCommentRequest
    {
        public string PublicationId { get; set; }
        public string Text { get; set; }
    }
}

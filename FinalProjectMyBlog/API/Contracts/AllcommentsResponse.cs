using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Contracts
{
    /// <summary>
    /// Вывод комментариев (модель ответа)
    /// </summary>
    public class AllcommentsResponse
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public string PublicationId { get; set; }
    }
}

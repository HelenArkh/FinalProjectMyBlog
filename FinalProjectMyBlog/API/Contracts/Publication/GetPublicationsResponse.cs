using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Contracts.Publication
{
    public class GetPublicationsResponse
    {
        public int PublicationAmount { get; set; }
        public PublicationView[] Publications { get; set; }
    }
        public class PublicationView
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }   
}

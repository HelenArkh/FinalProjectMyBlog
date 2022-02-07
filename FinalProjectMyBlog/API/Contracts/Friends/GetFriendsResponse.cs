using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Contracts.Friends
{
    public class GetFriendsResponse
    {
        public int FriendAmount { get; set; }
        public FriendView[] Friends { get; set; }
    }
        public class FriendView
    {
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }
    }   
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Contracts.Friends
{
    /// <summary>
    /// Удаляет друга.
    /// </summary>
    public class DeleteFriendRequest
    {
        public string Id { get; set; }
    }
}

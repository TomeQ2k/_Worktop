using System.Collections.Generic;
using Worktop.Core.Common.Helpers;

namespace Worktop.Core.Domain.Entities
{
    public class ChatRoom
    {
        public string Id { get; set; } = Utils.Id();
        public string RoomName { get; set; }
        public bool IsPassword { get; set; }
        public string Password { get; set; }
        public int? MaxClients { get; set; }
        public int CreatorId { get; set; }

        public User Creator { get; set; }

        public ICollection<Connection> Connections { get; set; } = new HashSet<Connection>();
        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }
}
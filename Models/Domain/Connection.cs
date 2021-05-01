using System;

namespace Worktop.Models.Domain
{
    public class Connection
    {
        public int UserId { get; set; }
        public string ConnectionId { get; set; }
        public DateTime DateEstablished { get; set; } = DateTime.Now;
        public string ChatRoomId { get; set; }

        public User User { get; set; }
        public ChatRoom ChatRoom { get; set; }

        public static Connection Create(int userId, string connId, string chatRoomId = null)
            => new Connection { UserId = userId, ConnectionId = connId, ChatRoomId = chatRoomId };
    }
}
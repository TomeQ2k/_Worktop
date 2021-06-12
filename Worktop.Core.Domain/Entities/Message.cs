using System;

namespace Worktop.Core.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateSent { get; set; } = DateTime.Now;
        public string SenderName { get; set; }
        public int SenderId { get; set; }
        public string ChatRoomId { get; set; }

        public User Sender { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }
}
using System;

namespace Worktop.Models.Domain
{
    public class Mail
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime DateSent { get; set; } = DateTime.Now;
        public bool IsFavorite { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public bool SenderDeleted { get; set; }
        public bool ReceiverDeleted { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
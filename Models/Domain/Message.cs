using System;
using Worktop.Core.Validators;
using Worktop.Core.Helpers;

namespace Worktop.Models.Domain
{
    public class Message
    {
        public int Id { get; set; }

        [RequiredValidator]
        [StringLengthValidator(Constants.MaxContentLength)]
        public string Text { get; set; }

        public DateTime DateSent { get; set; } = DateTime.Now;
        public string SenderName { get; set; }
        public int SenderId { get; set; }
        public string ChatRoomId { get; set; }

        public User Sender { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }
}
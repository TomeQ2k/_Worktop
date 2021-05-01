using Worktop.Core.Builders.Interfaces;
using Worktop.Models.Domain;

namespace Worktop.Core.Builders
{
    public class MessageBuilder : IMessageBuilder
    {
        private readonly Message message = new Message();

        public IMessageBuilder SetText(string text)
        {
            this.message.Text = text;

            return this;
        }

        public IMessageBuilder SentBy(int senderId, string senderName)
        {
            this.message.SenderId = senderId;
            this.message.SenderName = senderName;

            return this;
        }

        public IMessageBuilder InChatRoom(string roomId)
        {
            this.message.ChatRoomId = roomId;

            return this;
        }

        public Message Build() => this.message;
    }
}
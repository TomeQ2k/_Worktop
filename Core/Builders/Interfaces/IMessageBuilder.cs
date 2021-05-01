using Worktop.Models.Domain;

namespace Worktop.Core.Builders.Interfaces
{
    public interface IMessageBuilder : IBuilder<Message>
    {
        IMessageBuilder SetText(string text);
        IMessageBuilder SentBy(int senderId, string senderName);
        IMessageBuilder InChatRoom(string roomId);
    }
}
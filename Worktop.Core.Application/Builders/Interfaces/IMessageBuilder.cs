using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Builders.Interfaces
{
    public interface IMessageBuilder : IBuilder<Message>
    {
        IMessageBuilder SetText(string text);
        IMessageBuilder SentBy(int senderId, string senderName);
        IMessageBuilder InChatRoom(string roomId);
    }
}
using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels.Partials
{
    public class MessageCardViewModel
    {
        public Message Message { get; set; }

        public static MessageCardViewModel Build(Message message) => new MessageCardViewModel { Message = message };
    }
}
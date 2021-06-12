using Worktop.Core.Application.Models.Pagination;
using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels
{
    public class GlobalChatViewModel : BaseViewModel
    {
        public PagedList<Message> Messages { get; set; }

        public Message Message { get; set; }

        public GlobalChatViewModel(PagedList<Message> messages)
        {
            Title = "Global chat";

            Messages = messages;
        }
    }
}
using Worktop.Models.Helpers.Pagination;
using Worktop.Models.Domain;

namespace Worktop.ViewModels
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
using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels.Partials
{
    public class RoomCardViewModel : StringErrorViewModel
    {
        public ChatRoom Room { get; set; }

        public string Password { get; set; }

        public static RoomCardViewModel Build(ChatRoom room) => new RoomCardViewModel { Room = room };
    }
}
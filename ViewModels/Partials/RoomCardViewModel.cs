using Worktop.Models.Domain;

namespace Worktop.ViewModels.Partials
{
    public class RoomCardViewModel : StringErrorViewModel
    {
        public ChatRoom Room { get; set; }

        public string Password { get; set; }

        public static RoomCardViewModel Build(ChatRoom room) => new RoomCardViewModel { Room = room };
    }
}
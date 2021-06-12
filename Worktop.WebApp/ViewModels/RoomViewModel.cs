using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels
{
    public class RoomViewModel : BaseViewModel
    {
        public ChatRoom Room { get; set; }

        public Message Message { get; set; }

        public RoomViewModel(ChatRoom room)
        {
            Title = "Room";

            Room = room;
        }
    }
}
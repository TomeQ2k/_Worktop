using System.Collections.Generic;
using System.Linq;
using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels
{
    public class RoomsViewModel : BaseViewModel
    {
        public List<ChatRoom> Rooms { get; set; }

        public RoomsViewModel(IEnumerable<ChatRoom> rooms)
        {
            Title = "Chat rooms";

            Rooms = rooms.ToList();
        }
    }
}
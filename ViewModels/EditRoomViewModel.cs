using System.ComponentModel.DataAnnotations;
using Worktop.Core.Validators;
using Worktop.Core.Helpers;

namespace Worktop.ViewModels
{
    public class EditRoomViewModel : BaseViewModel
    {
        public string Id { get; set; }
        public int CreatorId { get; set; }

        [RequiredValidator]
        [StringLengthValidator(Constants.MaxRoomNameLength)]
        public string RoomName { get; set; }

        [StringLengthValidator(Constants.MaxPasswordLength)]
        public string Password { get; set; }

        [Range(Constants.MinClients, Constants.MaxClients)]
        public int? MaxClients { get; set; }

        public EditRoomViewModel()
        {
            Title = "Room";
        }
    }
}
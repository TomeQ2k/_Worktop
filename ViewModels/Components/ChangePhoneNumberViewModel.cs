using System.ComponentModel.DataAnnotations;

namespace Worktop.ViewModels.Components
{
    public class ChangePhoneNumberViewModel
    {
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
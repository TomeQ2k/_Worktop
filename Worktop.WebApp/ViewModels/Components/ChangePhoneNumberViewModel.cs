using System.ComponentModel.DataAnnotations;

namespace Worktop.WebApp.ViewModels.Components
{
    public class ChangePhoneNumberViewModel
    {
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
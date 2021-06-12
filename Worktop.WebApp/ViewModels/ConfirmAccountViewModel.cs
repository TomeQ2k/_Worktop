using System.ComponentModel.DataAnnotations;
using Worktop.Core.Application.Validators;

namespace Worktop.WebApp.ViewModels
{
    public class ConfirmAccountViewModel : BaseViewModel
    {
        [RequiredValidator]
        [EmailAddress]
        public string Email { get; set; }

        [RequiredValidator]
        public string Token { get; set; }

        public ConfirmAccountViewModel()
        {
            Title = "Confirm account";
        }
    }
}
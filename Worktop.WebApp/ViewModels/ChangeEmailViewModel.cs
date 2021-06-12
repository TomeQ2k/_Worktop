using System.ComponentModel.DataAnnotations;
using Worktop.Core.Application.Validators;

namespace Worktop.WebApp.ViewModels
{
    public class ChangeEmailViewModel : BaseViewModel
    {
        [RequiredValidator]
        [EmailAddress]
        public string NewEmail { get; set; }

        [RequiredValidator]
        public string Token { get; set; }

        public ChangeEmailViewModel()
        {
            Title = "Change email";
        }
    }
}
using System.ComponentModel.DataAnnotations;
using Worktop.Core.Application.Validators;

namespace Worktop.WebApp.ViewModels
{
    public class ResetPasswordCallbackViewModel : ErrorBaseViewModel
    {
        [RequiredValidator]
        [EmailAddress]
        public string Email { get; set; }

        [RequiredValidator]
        public string Token { get; set; }

        [RequiredValidator]
        [WhitespacesNotAllowedValidator]
        public string NewPassword { get; set; }

        public ResetPasswordCallbackViewModel()
        {
            Title = "Reset password";
        }
    }
}
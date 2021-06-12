using System.ComponentModel.DataAnnotations;
using Worktop.Core.Application.Validators;
using Worktop.Core.Common.Helpers;

namespace Worktop.WebApp.ViewModels
{
    public class ResetPasswordViewModel : ErrorBaseViewModel
    {
        [RequiredValidator]
        [EmailAddress]
        public string Email { get; set; }

        [RequiredValidator]
        [DataType(DataType.Password)]
        [StringLengthValidator(Constants.MaxPasswordLength, Constants.MinPasswordLength)]
        [WhitespacesNotAllowedValidator]
        public string NewPassword { get; set; }

        public ResetPasswordViewModel()
        {
            Title = "Reset password";
        }
    }
}
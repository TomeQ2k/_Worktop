using System.ComponentModel.DataAnnotations;
using Worktop.Core.Validators;
using Worktop.Core.Helpers;

namespace Worktop.ViewModels
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
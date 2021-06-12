using System.ComponentModel.DataAnnotations;
using Worktop.Core.Application.Validators;
using Worktop.Core.Common.Helpers;

namespace Worktop.WebApp.ViewModels
{
    public class RegisterViewModel : ErrorBaseViewModel
    {
        [RequiredValidator]
        [EmailAddress]
        public string Email { get; set; }

        [RequiredValidator]
        [StringLengthValidator(Constants.MaxUserNameLength, Constants.MinUserNameLength)]
        public string UserName { get; set; }

        [RequiredValidator]
        [DataType(DataType.Password)]
        [StringLengthValidator(Constants.MaxPasswordLength, Constants.MinPasswordLength)]
        [WhitespacesNotAllowedValidator]
        public string Password { get; set; }

        public RegisterViewModel()
        {
            Title = "Register";
        }
    }
}
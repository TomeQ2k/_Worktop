using System.ComponentModel.DataAnnotations;
using Worktop.Core.Application.Validators;
using Worktop.Core.Common.Helpers;

namespace Worktop.WebApp.ViewModels.Components
{
    public class ChangePasswordViewModel
    {
        [RequiredValidator]
        public string OldPassword { get; set; }

        [RequiredValidator]
        [DataType(DataType.Password)]
        [StringLengthValidator(Constants.MaxPasswordLength, Constants.MinPasswordLength)]
        [WhitespacesNotAllowedValidator]
        public string NewPassword { get; set; }
    }
}
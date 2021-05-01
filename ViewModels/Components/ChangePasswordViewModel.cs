using System.ComponentModel.DataAnnotations;
using Worktop.Core.Validators;
using Worktop.Core.Helpers;

namespace Worktop.ViewModels.Components
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
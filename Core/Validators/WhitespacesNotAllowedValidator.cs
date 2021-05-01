using System.ComponentModel.DataAnnotations;
using Worktop.Core.Extensions;
using Worktop.Core.Helpers;

namespace Worktop.Core.Validators
{
    public class WhitespacesNotAllowedValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string val = (string)value;

            if (val.HasWhitespaces())
                return new ValidationResult(ValidatorMessages.WhitespacesValidatorMessage);

            return ValidationResult.Success;
        }
    }
}
using System.ComponentModel.DataAnnotations;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Application.Helpers;

namespace Worktop.Core.Application.Validators
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
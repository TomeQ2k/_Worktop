using System.ComponentModel.DataAnnotations;
using Worktop.Core.Application.Helpers;

namespace Worktop.Core.Application.Validators
{
    public class RequiredValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult(ValidatorMessages.RequiredValidatorMessage);

            return ValidationResult.Success;
        }
    }
}
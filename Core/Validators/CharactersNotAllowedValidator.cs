using System.ComponentModel.DataAnnotations;
using System.Linq;
using Worktop.Core.Helpers;

namespace Worktop.Core.Validators
{
    public class CharactersNotAllowedValidator : ValidationAttribute
    {
        private readonly char[] charactersNotAllowed;

        public CharactersNotAllowedValidator(params char[] charactersNotAllowed)
        {
            this.charactersNotAllowed = charactersNotAllowed;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string str = (string)value;

            if (str != null && str.Any(c => charactersNotAllowed.Contains(c)))
                return new ValidationResult(ValidatorMessages.CharactersValidatorMessage(charactersNotAllowed));

            return ValidationResult.Success;
        }
    }
}
using System.ComponentModel.DataAnnotations;
using Worktop.Core.Application.Helpers;

namespace Worktop.Core.Application.Validators
{
    public class StringLengthValidator : ValidationAttribute
    {
        private int minLength;
        public int MinLength
        {
            get => minLength >= 0 ? minLength : 0;
            private set => minLength = value >= 0 ? value : 0;
        }

        public int MaxLength { get; }

        public StringLengthValidator(int maxLength, int minLength = 0)
        {
            MaxLength = maxLength;
            MinLength = minLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string val = (string)value;

            if (val?.Length > MaxLength || val?.Length < MinLength)
                return new ValidationResult(ValidatorMessages.StringLengthValidatorMessage(MinLength, MaxLength));

            return ValidationResult.Success;
        }
    }
}
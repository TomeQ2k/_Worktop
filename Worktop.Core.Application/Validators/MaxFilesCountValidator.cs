using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Worktop.Core.Application.Validators
{
    public class MaxFilesCountValidator : ValidationAttribute
    {
        private readonly int maxFilesCount;

        public MaxFilesCountValidator(int maxFilesCount)
        {
            this.maxFilesCount = maxFilesCount;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var files = value as List<IFormFile>;

            if (files.Count > maxFilesCount)
                return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }

        #region private

        private string GetErrorMessage() => $"Maximum files count is: {maxFilesCount}";

        #endregion
    }
}
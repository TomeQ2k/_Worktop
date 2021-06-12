using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Worktop.Core.Application.Validators
{
    public class MaxFileSizeValidator : ValidationAttribute
    {
        private readonly int maxFileSize;
        private readonly bool isCollection;

        public MaxFileSizeValidator(int maxFileSize, bool isCollection = false)
        {
            this.maxFileSize = maxFileSize;
            this.isCollection = isCollection;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!isCollection)
            {
                var file = value as IFormFile;

                if (file != null && file.Length > maxFileSize * 1024 * 1024)
                    return new ValidationResult(GetErrorMessage());
            }
            else
            {
                var files = value as List<IFormFile>;

                if (files != null)
                    foreach (var file in files)
                        if (file.Length > maxFileSize * 1024 * 1024)
                            return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }

        #region private

        private string GetErrorMessage() => $"Maximum file size is: {maxFileSize} MB";

        #endregion
    }
}
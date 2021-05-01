using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Worktop.Core.Validators;
using Worktop.Core.Helpers;

namespace Worktop.ViewModels.Components
{
    public class UploadFormViewModel
    {
        [RequiredValidator]
        [MaxFilesCountValidator(Constants.MaxFilesCount)]
        [MaxFileSizeValidator(Constants.MaxFileSize, isCollection: true)]
        [DataType(DataType.Upload)]
        public IEnumerable<IFormFile> Files { get; set; } = new List<IFormFile>();

        public string DirectoryId { get; set; }
        public bool IsPrivate { get; set; }

        public static UploadFormViewModel Build(string directoryId = null, bool isPrivate = false) => new UploadFormViewModel
        {
            DirectoryId = directoryId,
            IsPrivate = isPrivate
        };
    }
}
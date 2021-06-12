using Worktop.Core.Application.Validators;
using Worktop.Core.Common.Helpers;

namespace Worktop.WebApp.ViewModels.Components
{
    public class DirectoryFormViewModel
    {
        [RequiredValidator]
        [StringLengthValidator(Constants.MaxDirectoryLength)]
        [CharactersNotAllowedValidator('#')]
        public string DirectoryName { get; set; }

        public string DirectoryPath { get; set; }
        public string DirectoryId { get; set; }
        public bool IsPrivate { get; set; }

        public static DirectoryFormViewModel Build(string directoryPath, string directoryId = null, bool isPrivate = false) => new DirectoryFormViewModel
        {
            DirectoryPath = directoryPath,
            DirectoryId = directoryId,
            IsPrivate = isPrivate
        };
    }
}
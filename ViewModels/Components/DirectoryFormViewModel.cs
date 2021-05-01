using Worktop.Core.Validators;
using Worktop.Core.Helpers;

namespace Worktop.ViewModels.Components
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
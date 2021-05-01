using Worktop.Models.Domain;

namespace Worktop.ViewModels
{
    public class DirectoryViewModel : ErrorBaseViewModel
    {
        public Directory Directory { get; set; }

        public bool IsPrivate { get; set; }

        public DirectoryViewModel(Directory directory)
        {
            Title = "Directory";

            Directory = directory;
        }

        public DirectoryViewModel SetPrivate(bool isPrivate = false)
        {
            IsPrivate = isPrivate;

            return this;
        }
    }
}
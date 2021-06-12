using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels
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
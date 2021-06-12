using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels.Partials
{
    public class DirectoryCardViewModel
    {
        public Directory Directory { get; private set; }

        public bool IsCurrentDirectory { get; private set; }

        public static DirectoryCardViewModel Build(Directory directory, bool isCurrentDirectory)
            => new DirectoryCardViewModel
            {
                Directory = directory,
                IsCurrentDirectory = isCurrentDirectory
            };
    }
}
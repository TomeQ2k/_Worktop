using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels.Partials
{
    public class DirectoryCardViewModel
    {
        public Directory Directory { get; private set; }

        public static DirectoryCardViewModel Build(Directory directory) => new DirectoryCardViewModel { Directory = directory };
    }
}
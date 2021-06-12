using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels.Partials
{
    public class FileCardViewModel
    {
        public File File { get; set; }

        public static FileCardViewModel Build(File file) => new FileCardViewModel { File = file };
    }
}
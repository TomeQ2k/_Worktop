using System.Collections.Generic;

namespace Worktop.ViewModels
{
    public class StorageViewModel : ErrorBaseViewModel
    {
        public List<Models.Domain.File> Files { get; set; }
        public List<Models.Domain.Directory> Directories { get; set; }

        public StorageViewModel(List<Models.Domain.File> files, List<Models.Domain.Directory> directories)
        {
            Title = "Storage";

            Files = files;
            Directories = directories;
        }
    }
}
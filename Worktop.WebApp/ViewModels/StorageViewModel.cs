using System.Collections.Generic;
using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels
{
    public class StorageViewModel : ErrorBaseViewModel
    {
        public List<File> Files { get; set; }
        public List<Directory> Directories { get; set; }

        public StorageViewModel(List<File> files, List<Directory> directories)
        {
            Title = "Storage";

            Files = files;
            Directories = directories;
        }
    }
}
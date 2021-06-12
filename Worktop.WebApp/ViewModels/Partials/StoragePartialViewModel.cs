using System.Collections.Generic;
using System.Linq;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels.Partials
{
    public class StoragePartialViewModel
    {
        public List<File> Files { get; set; }
        public List<Directory> Directories { get; set; }

        public StorageSortType SortType { get; set; } = StorageSortType.DateDescending;

        public string DirectoryId { get; set; }
        public bool IsPrivate { get; set; }

        public static StoragePartialViewModel Build(IEnumerable<File> files, IEnumerable<Directory> directories, string directoryId = null, bool isPrivate = false)
            => new StoragePartialViewModel
            {
                Files = files.ToList(),
                Directories = directories.ToList(),
                DirectoryId = directoryId,
                IsPrivate = isPrivate
            };
    }
}
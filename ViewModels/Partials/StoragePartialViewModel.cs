using System.Collections.Generic;
using System.Linq;
using Worktop.Core.Enums;

namespace Worktop.ViewModels.Partials
{
    public class StoragePartialViewModel
    {
        public List<Models.Domain.File> Files { get; set; }
        public List<Models.Domain.Directory> Directories { get; set; }

        public StorageSortType SortType { get; set; } = StorageSortType.DateDescending;

        public string DirectoryId { get; set; }
        public bool IsPrivate { get; set; }

        public static StoragePartialViewModel Build(IEnumerable<Models.Domain.File> files, IEnumerable<Models.Domain.Directory> directories, string directoryId = null, bool isPrivate = false)
            => new StoragePartialViewModel
            {
                Files = files.ToList(),
                Directories = directories.ToList(),
                DirectoryId = directoryId,
                IsPrivate = isPrivate
            };
    }
}
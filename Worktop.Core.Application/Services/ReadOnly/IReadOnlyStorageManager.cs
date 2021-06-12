using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Application.Models.File;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyStorageManager
    {
        Task<DownloadContent> DownloadFile(string fileId);

        Task<Directory> ListDirectory(string directoryId, StorageSortType sortType = StorageSortType.DateDescending);
        Task<List<File>> ListFiles(string directoryId = null, bool isPrivate = false, StorageSortType sortType = StorageSortType.DateDescending);
        Task<List<Directory>> ListDirectories(string parentDirectoryId = null, bool isPrivate = false);
        Task<Directory> ListPreviousDirectory(string directoryId);
    }
}
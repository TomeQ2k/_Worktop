using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Worktop.Core.Enums;
using Worktop.Models.Helpers.File;
using Worktop.Models.Domain;

namespace Worktop.Core.Services.Interfaces
{
    public interface IStorageManager
    {
        Task<bool> UploadFile(IFormFile file, string directoryId = null, bool isPrivate = false);
        Task<bool> UploadFiles(IEnumerable<IFormFile> files, string directoryId = null, bool isPrivate = false);

        Task<DownloadContent> DownloadFile(string fileId);

        Task<Directory> ListDirectory(string directoryId, StorageSortType sortType = StorageSortType.DateDescending);
        Task<List<File>> ListFiles(string directoryId = null, bool isPrivate = false, StorageSortType sortType = StorageSortType.DateDescending);
        Task<List<Directory>> ListDirectories(string parentDirectoryId = null, bool isPrivate = false);
        Task<Directory> ListPreviousDirectory(string directoryId);

        Task<bool> DeleteFile(string fileId);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Worktop.Core.Application.Services.ReadOnly;

namespace Worktop.Core.Application.Services
{
    public interface IStorageManager : IReadOnlyStorageManager
    {
        Task<bool> UploadFile(IFormFile file, string directoryId = null, bool isPrivate = false);
        Task<bool> UploadFiles(IEnumerable<IFormFile> files, string directoryId = null, bool isPrivate = false);

        Task<bool> DeleteFile(string fileId);
    }
}
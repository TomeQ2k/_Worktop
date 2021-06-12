using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Worktop.Core.Application.Models.File;

namespace Worktop.Core.Application.Services
{
    public interface IFileWriter : IFileManagerBase
    {
        Task<FileModel> Upload(IFormFile file, string filePath = null);
        Task<IList<FileModel>> Upload(IEnumerable<IFormFile> files, string filePath = null);

        void Delete(string filePath);

        bool CreateDirectory(string directoryPath);
        bool DeleteDirectory(string directoryPath, bool recursive = true);
        string MoveDirectory(string oldPath, string newPath);
    }
}
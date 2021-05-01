using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Worktop.Models.Helpers.File;

namespace Worktop.Core.Services.Interfaces
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
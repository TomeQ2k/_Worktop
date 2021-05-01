using System.IO;
using System.Threading.Tasks;

namespace Worktop.Core.Services.Interfaces
{
    public interface IFileReader : IFileManagerBase
    {
        Task<string> ReadFile(string filePath);

        Task<byte[]> Download(string filePath);

        bool DirectoryExists(string directoryPath) => Directory.Exists(directoryPath);
    }
}
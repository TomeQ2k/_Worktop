using System.Threading.Tasks;

namespace Worktop.Core.Application.Services
{
    public interface IFileReader : IFileManagerBase
    {
        Task<string> ReadFile(string filePath);

        Task<byte[]> Download(string filePath);

        bool DirectoryExists(string directoryPath);
    }
}
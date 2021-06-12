using System.Threading.Tasks;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
{
    public interface IDirectoryManager
    {
        Task<Directory> GetDirectory(string directoryId);

        Task<Directory> CreateDirectory(string name, string directoryPath, bool isPrivate = false, string parentDirectoryId = null);
        Task<string> DeleteDirectory(string directoryId);
    }
}
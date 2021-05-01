using System.Threading.Tasks;

namespace Worktop.Core.Services.Interfaces
{
    public interface IDirectoryManager
    {
        Task<Models.Domain.Directory> GetDirectory(string directoryId);

        Task<Models.Domain.Directory> CreateDirectory(string name, string directoryPath, bool isPrivate = false, string parentDirectoryId = null);
        Task<bool> UpdateDirectory(string directoryId, string name);
        Task<string> DeleteDirectory(string directoryId);
    }
}
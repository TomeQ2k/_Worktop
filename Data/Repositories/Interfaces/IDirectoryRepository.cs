using System.Threading.Tasks;
using Worktop.Models.Domain;

namespace Worktop.Data.Repositories.Interfaces
{
    public interface IDirectoryRepository : IRepository<Directory>
    {
        Task<Directory> FindDirectoryWithParent(string directoryId);
        Task<Directory> FindDirectoryWithContent(string directoryId, int? currentUserId);
        Task<Directory> FindDirectoryWithParentAndUserId(string directoryId, int? currentUserId);
        Task<Directory> FindPreviousDirectory(string directoryId, int? currentUserId);
    }
}
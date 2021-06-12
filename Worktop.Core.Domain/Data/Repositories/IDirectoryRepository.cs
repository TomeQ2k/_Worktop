using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Domain.Data.Repositories
{
    public interface IDirectoryRepository : IRepository<Directory>
    {
        Task<Directory> FindDirectoryWithParent(string directoryId);
        Task<Directory> FindDirectoryWithContent(string directoryId, int? currentUserId);
        Task<Directory> FindDirectoryWithParentAndUserId(string directoryId, int? currentUserId);
        Task<Directory> FindPreviousDirectory(string directoryId, int? currentUserId);

        Task<IEnumerable<Directory>> GetParentDirectoriesByUserId(string parentDirectoryId, int? userId = default);
    }
}
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Worktop.Data.Repositories.Interfaces;
using Worktop.Models.Domain;

namespace Worktop.Data.Repositories
{
    public class DirectoryRepository : Repository<Directory>, IDirectoryRepository
    {
        public DirectoryRepository(DataContext context) : base(context) { }

        public async Task<Directory> FindDirectoryWithParent(string directoryId)
            => await context.Directories
                .Include(d => d.ParentDirectory)
                .FirstOrDefaultAsync(d => d.Id == directoryId);

        public async Task<Directory> FindDirectoryWithContent(string directoryId, int? currentUserId)
            => await context.Directories
                .Include(d => d.Files)
                .Include(d => d.Subdirectories)
                .FirstOrDefaultAsync(d => d.Id == directoryId && (d.UserId == null || d.UserId == currentUserId));

        public async Task<Directory> FindDirectoryWithParentAndUserId(string directoryId, int? currentUserId)
            => await context.Directories
                .Include(d => d.ParentDirectory)
                .FirstOrDefaultAsync(d => d.Id == directoryId && (d.UserId == null || d.UserId == currentUserId));

        public async Task<Directory> FindPreviousDirectory(string directoryId, int? currentUserId)
            => (await context.Directories
                .Include(d => d.ParentDirectory)
                .FirstOrDefaultAsync(d => d.Id == directoryId && (d.UserId == null || (d.UserId != null && d.UserId == currentUserId))))?.ParentDirectory;
    }
}
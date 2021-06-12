using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Worktop.Core.Domain.Data.Repositories;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Persistence.Database.Repositories
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

        public async Task<IEnumerable<Directory>> GetParentDirectoriesByUserId(string parentDirectoryId, int? userId = default)
            => await context.Directories
                .Where(d => d.ParentDirectoryId == parentDirectoryId && d.UserId == userId)
                .OrderByDescending(d => d.DateUpdated)
                .ToListAsync();
    }
}
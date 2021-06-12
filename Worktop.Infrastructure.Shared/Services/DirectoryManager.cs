using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Worktop.Core.Application.Builders;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Application.Models.Alert;
using Worktop.Core.Application.Services;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Data;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Shared.Services
{
    public class DirectoryManager : IDirectoryManager
    {
        private readonly IFileWriter fileWriter;
        private readonly IFileReader fileReader;
        private readonly IFilePathBuilder filePathBuilder;
        private readonly IDatabase database;

        private readonly int? currentUserId;

        public DirectoryManager(IFileWriter fileWriter, IFileReader fileReader, IFilePathBuilder filePathBuilder, IDatabase database, IHttpContextAccessor httpContextAccessor)
        {
            this.fileWriter = fileWriter;
            this.fileReader = fileReader;
            this.filePathBuilder = filePathBuilder;
            this.database = database;

            this.currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
        }

        public async Task<Directory> GetDirectory(string directoryId)
            => await database.DirectoryRepository.FindDirectoryWithParentAndUserId(directoryId, currentUserId);

        public async Task<Directory> CreateDirectory(string name, string directoryPath, bool isPrivate = false, string parentDirectoryId = null)
        {
            string fullPath = $"{directoryPath}{name}";

            if (!fileWriter.CreateDirectory(fullPath))
            {
                ErrorWriter.Append("Directory name already exists");
                return null;
            }

            var directory = new DirectoryBuilder()
                                .SetName(name)
                                .SetPath(fullPath)
                                .AssignedTo(!isPrivate ? null : currentUserId)
                                .WithParentDirectory(parentDirectoryId)
                                .Build();

            database.DirectoryRepository.Add(directory);

            return await database.Complete() ? directory : null;
        }

        public async Task<string> DeleteDirectory(string directoryId)
        {
            var directory = await GetDirectory(directoryId);

            if (directory != null)
            {
                fileWriter.DeleteDirectory(directory.Path);

                database.DirectoryRepository.Delete(directory);

                return await database.Complete() ? directory.ParentDirectoryId ?? string.Empty : null;
            }

            Alertify.Push("Directory cannot be found", AlertType.Error);
            return null;
        }
    }
}
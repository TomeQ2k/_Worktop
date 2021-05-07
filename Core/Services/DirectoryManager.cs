using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Worktop.Core.Builders;
using Worktop.Data;
using Worktop.Core.Extensions;
using Worktop.Core.Services.Interfaces;
using Worktop.Core.Enums;

namespace Worktop.Core.Services
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

        public async Task<Models.Domain.Directory> GetDirectory(string directoryId)
            => await database.DirectoryRepository.FindDirectoryWithParentAndUserId(directoryId, currentUserId);

        public async Task<Models.Domain.Directory> CreateDirectory(string name, string directoryPath, bool isPrivate = false, string parentDirectoryId = null)
        {
            string fullPath = $"{directoryPath}{name}{(!isPrivate ? string.Empty : $"#{currentUserId.ToString()}")}";

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

        public async Task<bool> UpdateDirectory(string directoryId, string name)
        {
            var directory = await GetDirectory(directoryId);

            if (directory != null)
            {
                directory.Name = name;

                string newPath = directory.UserId == null
                    ? await filePathBuilder.BuildFilePath(directory, "public/")
                    : await filePathBuilder.BuildFilePath(directory, "private/");

                if (fileReader.DirectoryExists(newPath))
                {
                    ErrorWriter.Append("Directory name already exists");
                    return false;
                }

                fileWriter.DeleteDirectory(directory.Path);

                directory.DateUpdated = DateTime.Now;

                newPath = $"{newPath.Remove(newPath.Length - 1)}{(directory.UserId == null ? string.Empty : $"#{currentUserId.ToString()}")}";
                directory.Path = fileWriter.MoveDirectory(directory.Path, newPath);

                database.DirectoryRepository.Update(directory);

                return await database.Complete();
            }

            Alertify.Push("Directory cannot be found", AlertType.Error);
            return false;
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
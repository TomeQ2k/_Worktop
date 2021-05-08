using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Worktop.Core.Builders;
using Worktop.Data;
using Worktop.Core.Enums;
using Worktop.Core.Extensions;
using Worktop.Models.Helpers.File;
using Worktop.Core.Services.Interfaces;
using Worktop.Models.Domain;
using System;

namespace Worktop.Core.Services
{
    public class StorageManager : IStorageManager
    {
        private readonly IFileWriter fileWriter;
        private readonly IFileReader fileReader;
        private readonly IFilePathBuilder filePathBuilder;
        private readonly IDatabase database;
        private readonly IStorageSizeManager storageSizeManager;

        private readonly int? currentUserId;

        public StorageManager(IFileWriter fileWriter, IFileReader fileReader, IFilePathBuilder filePathBuilder, IDatabase database,
            IStorageSizeManager storageSizeManager, IHttpContextAccessor httpContextAccessor)
        {
            this.fileWriter = fileWriter;
            this.fileReader = fileReader;
            this.filePathBuilder = filePathBuilder;
            this.database = database;
            this.storageSizeManager = storageSizeManager;

            this.currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
        }

        public async Task<bool> UploadFile(IFormFile file, string directoryId = null, bool isPrivate = false)
        {
            if (file == null)
            {
                ErrorWriter.Append("You have to choose file to upload");
                return false;
            }

            if (await storageSizeManager.CountStorageSize(isPrivateStorage: isPrivate) + (uint)file.Length / StorageSizeManager.UnitConversionMultiplier
                > (!isPrivate ? storageSizeManager.MaxPublicStorageSizeInGb : storageSizeManager.MaxPrivateStorageSizeInGb) * Math.Pow(StorageSizeManager.UnitConversionMultiplier, 2))
            {
                Alertify.Push("Storage is full", AlertType.Warning);
                return false;
            }

            string filePath = !isPrivate ? @"public/" : $@"private/{currentUserId}/";

            if (!string.IsNullOrEmpty(directoryId))
            {
                var directory = await database.DirectoryRepository.FindDirectoryWithParent(directoryId);
                filePath = await filePathBuilder.BuildFilePath(directory, filePath);
            }

            var uploadedFile = await fileWriter.Upload(file, filePath);

            if (uploadedFile != null)
            {
                var fileToStore = new FileBuilder()
                            .SetName(file.FileName)
                            .SetPath(uploadedFile.Path)
                            .WithSize(uploadedFile.Size)
                            .InDirectory(directoryId)
                            .AssignedTo(userId: !isPrivate ? null : currentUserId)
                            .Build();

                database.FileRepository.Add(fileToStore);

                return await database.Complete();
            }

            Alertify.Push("File cannot be uploaded", AlertType.Error);
            return false;
        }

        public async Task<bool> UploadFiles(IEnumerable<IFormFile> files, string directoryId = null, bool isPrivate = false)
        {
            if (!files.Any())
            {
                ErrorWriter.Append("You have to choose files to upload");
                return false;
            }

            bool uploadResult = true;

            foreach (var file in files)
                uploadResult = uploadResult && await UploadFile(file, directoryId, isPrivate);

            return uploadResult;
        }

        public async Task<DownloadContent> DownloadFile(string fileId)
        {
            var file = await GetFile(fileId);

            if (file != null)
            {
                var downloadedFile = await fileReader.Download(file.Path);

                var downloadContent = new DownloadContent(downloadedFile, file.Name);

                return downloadContent;
            }

            return null;
        }

        public async Task<Directory> ListDirectory(string directoryId, StorageSortType sortType = StorageSortType.DateDescending)
        {
            var directory = await database.DirectoryRepository.FindDirectoryWithContent(directoryId, currentUserId);

            if (directory == null)
                return null;

            directory.Files = SortFiles(sortType, directory.Files);
            directory.Subdirectories = directory.Subdirectories.OrderByDescending(d => d.DateUpdated).ToList();

            return directory;
        }

        public async Task<List<Models.Domain.File>> ListFiles(string directoryId = null, bool isPrivate = false, StorageSortType sortType = StorageSortType.DateDescending)
        {
            var files = (!isPrivate
                ? await database.FileRepository.Filter(f => f.UserId == null && f.DirectoryId == directoryId)
                : await database.FileRepository.Filter(f => f.UserId == currentUserId && f.DirectoryId == directoryId))
                .ToList();

            files = SortFiles(sortType, files);

            return files;
        }

        public async Task<List<Models.Domain.Directory>> ListDirectories(string parentDirectoryId = null, bool isPrivate = false)
            => (!isPrivate
                ? (await database.DirectoryRepository.Filter(d => d.UserId == null && d.ParentDirectoryId == parentDirectoryId))
                    .OrderByDescending(d => d.DateUpdated)
                : (await database.DirectoryRepository.Filter(d => d.UserId == currentUserId && d.ParentDirectoryId == parentDirectoryId))
                    .OrderByDescending(d => d.DateUpdated))
                .ToList();

        public async Task<Directory> ListPreviousDirectory(string directoryId)
            => await database.DirectoryRepository.FindPreviousDirectory(directoryId, currentUserId);

        public async Task<bool> DeleteFile(string fileId)
        {
            var file = await GetFile(fileId);

            if (file != null)
            {
                fileWriter.Delete(file.Path);

                database.FileRepository.Delete(file);

                return await database.Complete();
            }

            Alertify.Push("File cannot be found", AlertType.Error);
            return false;
        }

        #region private

        private async Task<Models.Domain.File> GetFile(string fileId)
            => await database.FileRepository.Find(f => f.Id == fileId && (f.UserId == null || f.UserId == currentUserId));

        private static List<File> SortFiles(StorageSortType sortType, IEnumerable<File> files)
            => (sortType switch
            {
                StorageSortType.DateDescending => files.OrderByDescending(f => f.DateCreated),
                StorageSortType.DateAscending => files.OrderBy(f => f.DateCreated),
                StorageSortType.NameAscending => files.OrderBy(f => f.Name.ToLowerInvariant()),
                StorageSortType.NameDescending => files.OrderByDescending(f => f.Name.ToLowerInvariant()),
                _ => files
            }).ToList();

        #endregion
    }
}
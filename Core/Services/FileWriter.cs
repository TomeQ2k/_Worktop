using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Worktop.Models.Helpers.File;
using Worktop.Core.Helpers;
using Worktop.Core.Services.Interfaces;

namespace Worktop.Core.Services
{
    public class FileWriter : IFileWriter
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public string ProjectPath => webHostEnvironment.ContentRootPath;
        public string WebRootPath => webHostEnvironment.WebRootPath;

        public IConfiguration Configuration { get; }

        public FileWriter(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            this.webHostEnvironment = webHostEnvironment;
            Configuration = configuration;
        }

        public async Task<FileModel> Upload(IFormFile file, string filePath = null)
            => await UploadFile(file, filePath);

        public async Task<IList<FileModel>> Upload(IEnumerable<IFormFile> files, string filePath = null)
        {
            var uploadFiles = new List<FileModel>();

            foreach (var file in files)
                uploadFiles.Add(await UploadFile(file, filePath));

            return uploadFiles;
        }

        public void Delete(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        public bool CreateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                return true;
            }

            return false;
        }

        public bool DeleteDirectory(string directoryPath, bool recursive = true)
        {
            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, recursive: recursive);
                return true;
            }

            return false;
        }

        public string MoveDirectory(string oldPath, string newPath)
        {
            Directory.Move(oldPath, newPath);
            return newPath;
        }

        #region private

        private async Task<FileModel> UploadFile(IFormFile file, string filePath)
        {
            if (file == null || file?.Length <= 0)
                return null;

            var uploadFile = BuildFileModel(filePath, Path.GetExtension(file.FileName), (uint)file.Length / StorageSizeManager.UnitConversionMultiplier);

            using (var stream = System.IO.File.Create(uploadFile.Path))
            {
                await file.CopyToAsync(stream);
            }

            return uploadFile;
        }

        private FileModel BuildFileModel(string filePath, string fileExtension, uint fileSize)
        {
            var fullPath = filePath == null ? $"{WebRootPath}/files/" : $"{WebRootPath}/files/{filePath}/";
            var fileUrl = filePath == null ? $"{Configuration.GetValue<string>(AppSettingsKeys.ServerAddress)}files/" : $"{Configuration.GetValue<string>(AppSettingsKeys.ServerAddress)}files/{filePath}/";

            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);

            string fileName = $"{Utils.NewGuid(length: 32)}{fileExtension}";

            fullPath += fileName;
            fileUrl += fileName;

            return new FileModel(fullPath, fileUrl, fileSize);
        }

        #endregion
    }
}
using System.IO;
using Microsoft.AspNetCore.StaticFiles;
using Worktop.Core.Application.Models.File;
using Worktop.Core.Application.Services;

namespace Worktop.Infrastructure.Shared.Services
{
    public class MimeMappingService : IMimeMappingService
    {
        private readonly FileExtensionContentTypeProvider contentTypeProvider;

        private const string DefaultContentType = "application/octet-stream";

        public MimeMappingService(FileExtensionContentTypeProvider contentTypeProvider)
        {
            this.contentTypeProvider = contentTypeProvider;
        }

        public string MapContentType(DownloadContent downloadContent)
        {
            string fileExtension = Path.GetExtension(downloadContent.FileName).ToLowerInvariant();

            string contentType = downloadContent.ContentTypesDictionary.ContainsKey(fileExtension)
                ? downloadContent.ContentTypesDictionary[fileExtension] : null;

            if (contentType == null && !contentTypeProvider.TryGetContentType(downloadContent.FileName, out contentType))
                contentType = DefaultContentType;

            return contentType;
        }
    }
}
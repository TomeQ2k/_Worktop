using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Worktop.Core.Services.Interfaces;

namespace Worktop.Core.Services
{
    public class FileReader : IFileReader
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public string ProjectPath => webHostEnvironment.ContentRootPath;
        public string WebRootPath => webHostEnvironment.WebRootPath;

        public IConfiguration Configuration { get; }

        public FileReader(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            this.webHostEnvironment = webHostEnvironment;
            Configuration = configuration;
        }

        public async Task<string> ReadFile(string filePath)
            => await File.ReadAllTextAsync($"{WebRootPath}/{filePath}");

        public async Task<byte[]> Download(string filePath)
        {
            filePath = $"{WebRootPath}/{filePath}";

            byte[] downloadedFile = default(byte[]);

            if (File.Exists(filePath))
                downloadedFile = await File.ReadAllBytesAsync(filePath);

            return downloadedFile;
        }

        public bool DirectoryExists(string directoryPath) => Directory.Exists($"{WebRootPath}/{directoryPath}");
    }
}
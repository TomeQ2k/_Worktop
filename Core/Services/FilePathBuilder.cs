using System.Collections.Generic;
using System.Linq;
using Worktop.Data;
using Worktop.Core.Services.Interfaces;
using Worktop.Models.Domain;
using System.Threading.Tasks;

namespace Worktop.Core.Services
{
    public class FilePathBuilder : IFilePathBuilder
    {
        private readonly IDatabase database;

        public FilePathBuilder(IDatabase database)
        {
            this.database = database;
        }

        public async Task<string> BuildFilePath(Directory directory, string path)
        {
            if (directory == null)
                return path;

            var parentDirectoriesStack = new Stack<string>();

            do
            {
                parentDirectoriesStack.Push($"{directory.Name}/");
                directory = await database.DirectoryRepository.Find(d => d.Id == directory.ParentDirectoryId);
            } while (directory?.Id != null);

            while (parentDirectoriesStack.Any())
                path += parentDirectoriesStack.Pop();

            return path;
        }
    }
}
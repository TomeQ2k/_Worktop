using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worktop.Core.Application.Services;
using Worktop.Core.Domain.Data;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Shared.Services
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
using Worktop.Core.Application.Builders.Interfaces;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Builders
{
    public class FileBuilder : IFileBuilder
    {
        private readonly File file = new File();

        public IFileBuilder SetName(string name)
        {
            this.file.Name = name;

            return this;
        }

        public IFileBuilder SetPath(string path)
        {
            this.file.Path = path;

            return this;
        }

        public IFileBuilder WithSize(uint size)
        {
            this.file.Size = size;

            return this;
        }

        public IFileBuilder InDirectory(string directoryId)
        {
            this.file.DirectoryId = directoryId;

            return this;
        }

        public IFileBuilder AssignedTo(int? userId)
        {
            this.file.UserId = userId;

            return this;
        }

        public File Build() => this.file;
    }
}
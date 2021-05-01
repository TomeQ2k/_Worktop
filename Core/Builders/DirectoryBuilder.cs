using Worktop.Core.Builders.Interfaces;
using Worktop.Models.Domain;

namespace Worktop.Core.Builders
{
    public class DirectoryBuilder : IDirectoryBuilder
    {
        private readonly Directory directory = new Directory();

        public IDirectoryBuilder SetName(string name)
        {
            this.directory.Name = name;

            return this;
        }

        public IDirectoryBuilder SetPath(string path)
        {
            this.directory.Path = path;

            return this;
        }

        public IDirectoryBuilder AssignedTo(int? userId)
        {
            this.directory.UserId = userId;

            return this;
        }

        public IDirectoryBuilder WithParentDirectory(string parentDirectoryId = null)
        {
            this.directory.ParentDirectoryId = parentDirectoryId;

            return this;
        }

        public Directory Build() => this.directory;
    }
}
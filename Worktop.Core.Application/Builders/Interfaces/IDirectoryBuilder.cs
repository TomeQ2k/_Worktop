using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Builders.Interfaces
{
    public interface IDirectoryBuilder : IBuilder<Directory>
    {
        IDirectoryBuilder SetName(string name);
        IDirectoryBuilder SetPath(string path);
        IDirectoryBuilder AssignedTo(int? userId);
        IDirectoryBuilder WithParentDirectory(string parentDirectoryId = null);
    }
}
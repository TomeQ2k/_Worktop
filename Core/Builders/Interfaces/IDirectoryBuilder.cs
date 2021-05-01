using Worktop.Models.Domain;

namespace Worktop.Core.Builders.Interfaces
{
    public interface IDirectoryBuilder : IBuilder<Directory>
    {
        IDirectoryBuilder SetName(string name);
        IDirectoryBuilder SetPath(string path);
        IDirectoryBuilder AssignedTo(int? userId);
        IDirectoryBuilder WithParentDirectory(string parentDirectoryId = null);
    }
}
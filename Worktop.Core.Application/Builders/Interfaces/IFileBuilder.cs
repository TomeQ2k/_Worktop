using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Builders.Interfaces
{
    public interface IFileBuilder : IBuilder<File>
    {
        IFileBuilder SetName(string name);
        IFileBuilder SetPath(string path);
        IFileBuilder WithSize(uint size);
        IFileBuilder InDirectory(string directoryId);
        IFileBuilder AssignedTo(int? userId);
    }
}
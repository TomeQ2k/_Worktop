namespace Worktop.Core.Builders.Interfaces
{
    public interface IFileBuilder : IBuilder<Models.Domain.File>
    {
        IFileBuilder SetName(string name);
        IFileBuilder SetPath(string path);
        IFileBuilder WithSize(uint size);
        IFileBuilder InDirectory(string directoryId);
        IFileBuilder AssignedTo(int? userId);
    }
}
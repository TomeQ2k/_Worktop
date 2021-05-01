namespace Worktop.Core.Services.Interfaces
{
    public interface IFileManagerBase
    {
        string ProjectPath { get; }
        string WebRootPath { get; }
    }
}
namespace Worktop.Core.Application.Services
{
    public interface IFileManagerBase
    {
        string ProjectPath { get; }
        string WebRootPath { get; }
    }
}
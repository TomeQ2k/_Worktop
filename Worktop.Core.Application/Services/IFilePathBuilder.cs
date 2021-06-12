using System.Threading.Tasks;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
{
    public interface IFilePathBuilder
    {
        Task<string> BuildFilePath(Directory directory, string path);
    }
}
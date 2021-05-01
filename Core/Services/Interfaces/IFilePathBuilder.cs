using System.Threading.Tasks;
using Worktop.Models.Domain;

namespace Worktop.Core.Services.Interfaces
{
    public interface IFilePathBuilder
    {
        Task<string> BuildFilePath(Directory directory, string path);
    }
}
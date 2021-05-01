using System.Threading.Tasks;

namespace Worktop.Core.Services.Interfaces
{
    public interface IJobService
    {
        Task<bool> InsertJobsFromFile();
    }
}
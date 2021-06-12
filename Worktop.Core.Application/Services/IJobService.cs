using System.Threading.Tasks;

namespace Worktop.Core.Application.Services
{
    public interface IJobService
    {
        Task<bool> InsertJobsFromFile();
    }
}
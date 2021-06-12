using System.Threading.Tasks;
using Worktop.Core.Application.Services.ReadOnly;

namespace Worktop.Core.Application.Services
{
    public interface IPanelManager : IReadOnlyPanelManager
    {
        Task<bool> AssignJob(int jobId, int workerId);

        Task<bool> DeleteWorker(int workerId);
    }
}
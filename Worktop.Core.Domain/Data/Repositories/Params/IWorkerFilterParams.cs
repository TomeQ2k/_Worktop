using Worktop.Core.Common.Enums;

namespace Worktop.Core.Domain.Data.Repositories.Params
{
    public interface IWorkerFilterParams
    {
        string UserName { get; set; }
        string Email { get; set; }
        WorkersSortType SortType { get; set; }
        bool IsAdmin { get; set; }
    }
}
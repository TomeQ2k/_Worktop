using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Data.Repositories.Params;

namespace Worktop.Core.Application.Params
{
    public class WorkerFiltersParams : FiltersParams, IWorkerFiltersParams
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public WorkersSortType SortType { get; set; }
        public bool IsAdmin { get; set; }

        public static WorkerFiltersParams Build(string username = null, string email = null, WorkersSortType sortType = WorkersSortType.None, bool isAdmin = false)
            => new WorkerFiltersParams
            {
                UserName = username,
                Email = email,
                SortType = sortType,
                IsAdmin = isAdmin
            };
    }
}
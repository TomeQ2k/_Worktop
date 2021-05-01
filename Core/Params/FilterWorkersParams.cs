using Worktop.Core.Enums;

namespace Worktop.Core.Params
{
    public class FilterWorkersParams : FilterParams
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public WorkersSortType SortType { get; private set; }
        public bool IsAdmin { get; private set; }

        public static FilterWorkersParams Build(string username = null, string email = null, WorkersSortType sortType = WorkersSortType.None, bool isAdmin = false)
            => new FilterWorkersParams
            {
                UserName = username,
                Email = email,
                SortType = sortType,
                IsAdmin = isAdmin
            };
    }
}
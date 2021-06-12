using Worktop.Core.Application.Models.Pagination;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels
{
    public class PanelViewModel : BaseViewModel
    {
        public PagedList<User> Workers { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public WorkersSortType SortType { get; set; } = WorkersSortType.None;
        public bool IsAdmin { get; set; }

        public PanelViewModel()
        {
            Title = "Panel";
        }

        public PanelViewModel(PagedList<User> workers)
        {
            Title = "Panel";

            Workers = workers;
        }

        public PanelViewModel FilterWorkers(PagedList<User> workers)
        {
            Workers = workers;

            return this;
        }
    }
}
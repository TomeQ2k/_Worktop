using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyTasksManager
    {
        Task<TaskItem> GetTask(int taskId);
        Task<IEnumerable<TaskItem>> GetUserTasks(int userId);
        Task<IEnumerable<TaskItem>> GetCurrentUserTasks();
    }
}
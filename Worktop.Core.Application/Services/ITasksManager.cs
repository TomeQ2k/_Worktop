using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
{
    public interface ITasksManager
    {
        Task<TaskItem> GetTask(int taskId);
        Task<IEnumerable<TaskItem>> GetUserTasks(int userId);
        Task<IEnumerable<TaskItem>> GetCurrentUserTasks();

        Task<bool> ArrangeTask(string description, DateTime dateDeadline);
        Task<bool> UpdateTask(int taskId, string description, DateTime dateDeadline);
        Task<bool> DeleteTask(int taskId);

        Task<bool> ChangeProgressStatus(int taskId, TaskProgress progress);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Enums;
using Worktop.Models.Domain;

namespace Worktop.Core.Services.Interfaces
{
    public interface ITasksManager
    {
        Task<TaskItem> GetTask(int taskId);
        Task<IEnumerable<TaskItem>> GetUserTasks();

        Task<bool> ArrangeTask(string description, DateTime dateDeadline);
        Task<bool> UpdateTask(int taskId, string description, DateTime dateDeadline);
        Task<bool> DeleteTask(int taskId);

        Task<bool> ChangeProgressStatus(int taskId, TaskProgress progress);
    }
}
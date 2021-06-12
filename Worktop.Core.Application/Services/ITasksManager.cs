using System;
using System.Threading.Tasks;
using Worktop.Core.Application.Services.ReadOnly;
using Worktop.Core.Common.Enums;

namespace Worktop.Core.Application.Services
{
    public interface ITasksManager : IReadOnlyTasksManager
    {
        Task<bool> ArrangeTask(string description, DateTime dateDeadline);
        Task<bool> UpdateTask(int taskId, string description, DateTime dateDeadline);
        Task<bool> DeleteTask(int taskId);

        Task<bool> ChangeProgressStatus(int taskId, TaskProgress progress);
    }
}
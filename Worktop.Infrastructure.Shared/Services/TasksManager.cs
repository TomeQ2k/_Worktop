using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Worktop.Core.Application.Builders;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Application.Services;
using Worktop.Core.Application.Services.ReadOnly;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Data;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Shared.Services
{
    public class TasksManager : ITasksManager, ICanExecute<TaskItem>
    {
        private readonly IDatabase database;
        private readonly IReadOnlyRolesService rolesService;

        private readonly int currentUserId;

        public TasksManager(IDatabase database, IHttpContextAccessor httpContextAccessor, IReadOnlyRolesService rolesService)
        {
            this.database = database;
            this.rolesService = rolesService;

            this.currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
        }

        public async Task<TaskItem> GetTask(int taskId)
            => await database.TaskRepository.Find(t => t.Id == taskId && (t.ExecutorId == currentUserId || t.ExecutorId == null));

        public async Task<IEnumerable<TaskItem>> GetUserTasks(int userId)
            => await database.TaskRepository.GetWhere(t => t.ExecutorId == userId);

        public async Task<IEnumerable<TaskItem>> GetCurrentUserTasks()
            => await database.TaskRepository.GetWhere(t => t.ExecutorId == currentUserId || t.Progress == TaskProgress.Unassigned);

        public async Task<bool> ArrangeTask(string description, DateTime dateDeadline)
        {
            var task = !await rolesService.IsPermitted(RoleName.Admin, currentUserId)
                ? new TaskItemBuilder()
                    .SetDescription(description)
                    .SetDeadline(dateDeadline)
                    .WithExecutor(currentUserId)
                    .SetProgress(TaskProgress.Assigned)
                    .Build()
                : new TaskItemBuilder()
                    .SetDescription(description)
                    .SetDeadline(dateDeadline)
                    .WithExecutor()
                    .SetProgress()
                    .Build();

            database.TaskRepository.Add(task);

            return await database.Complete();
        }

        public async Task<bool> UpdateTask(int taskId, string description, DateTime dateDeadline)
        {
            var task = await GetTask(taskId);

            if (task == null)
                return false;

            if (!await CanExecute(task))
                return false;

            task.Description = description;
            task.DateDeadline = dateDeadline;

            database.TaskRepository.Update(task);

            return await database.Complete();
        }

        public async Task<bool> DeleteTask(int taskId)
        {
            var task = await GetTask(taskId);

            if (task == null)
                return false;

            if (!await CanExecute(task))
                return false;

            database.TaskRepository.Delete(task);

            return await database.Complete();
        }

        public async Task<bool> ChangeProgressStatus(int taskId, TaskProgress progress)
        {
            var task = await GetTask(taskId);

            if (task == null)
                return false;

            if (task.ExecutorId != null && !await CanExecute(task))
                return false;

            task.ExecutorId = progress != TaskProgress.Unassigned ? (int?)currentUserId : null;

            task.Progress = progress;

            database.TaskRepository.Update(task);

            return await database.Complete();
        }

        public async Task<bool> CanExecute(TaskItem item)
            => item != null && !(!await rolesService.IsPermitted(RoleName.Admin, currentUserId) && item.ExecutorId != currentUserId);
    }
}
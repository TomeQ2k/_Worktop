using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Worktop.Core.Builders;
using Worktop.Data;
using Worktop.Core.Enums;
using Worktop.Core.Extensions;
using Worktop.Core.Services.Interfaces;
using Worktop.Models.Domain;

namespace Worktop.Core.Services
{
    public class TasksManager : ITasksManager, ICanExecute<TaskItem>
    {
        private readonly IDatabase database;
        private readonly IRolesService rolesService;

        private readonly int currentUserId;

        public TasksManager(IDatabase database, IHttpContextAccessor httpContextAccessor, IRolesService rolesService)
        {
            this.database = database;
            this.rolesService = rolesService;

            this.currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
        }

        public async Task<TaskItem> GetTask(int taskId) => await rolesService.IsPermitted(RoleName.Admin, currentUserId)
            ? await database.TaskRepository.Get(taskId)
            : await database.TaskRepository.Find(t => t.Id == taskId && (t.ExecutorId == currentUserId || t.ExecutorId == null));

        public async Task<IEnumerable<TaskItem>> GetUserTasks()
            => await database.TaskRepository.Filter(t => t.ExecutorId == currentUserId || t.Progress == TaskProgress.Unassigned);

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

            if (!await CanExecute(task))
                return false;

            database.TaskRepository.Delete(task);

            return await database.Complete();
        }

        public async Task<bool> ChangeProgressStatus(int taskId, TaskProgress progress)
        {
            var task = await GetTask(taskId);

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
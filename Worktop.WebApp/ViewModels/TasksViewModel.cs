using System;
using System.Collections.Generic;
using System.Linq;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        public List<TaskItem> UnassignedTasks { get; private set; }
        public List<TaskItem> AssignedTasks { get; private set; }
        public List<TaskItem> InProgressTasks { get; private set; }
        public List<TaskItem> CompletedTasks { get; private set; }
        public List<TaskItem> NotCompletedTasks { get; } = new List<TaskItem>();

        public Dictionary<int, string> WorkersDictionary { get; } = new Dictionary<int, string>();

        public string SelectedWorkerName { get; private set; }
        public bool ReadOnly { get; private set; }

        public TasksViewModel()
        {
            Title = "Tasks";
        }

        public static TasksViewModel Create(IEnumerable<TaskItem> tasks, IEnumerable<User> workers)
            => new TasksViewModel().FilterTasks(tasks).SetWorkersDictionary(workers);

        public TasksViewModel SetReadOnly()
        {
            ReadOnly = true;
            return this;
        }

        public TasksViewModel SelectWorker(int workerId)
        {
            SelectedWorkerName = WorkersDictionary[workerId];
            return this;
        }

        #region private

        private TasksViewModel FilterTasks(IEnumerable<TaskItem> tasks)
        {
            tasks.ToList().ForEach(t =>
            {
                if (t.DateDeadline.AddDays(1) < DateTime.Now)
                {
                    t.NotCompleted = true;
                    NotCompletedTasks.Add(t);
                }
            });

            tasks = tasks.Except(NotCompletedTasks).ToList();

            UnassignedTasks = tasks.Where(t => t.Progress == TaskProgress.Unassigned).ToList();
            AssignedTasks = tasks.Where(t => t.Progress == TaskProgress.Assigned).ToList();
            InProgressTasks = tasks.Where(t => t.Progress == TaskProgress.InProgress).ToList();
            CompletedTasks = tasks.Where(t => t.Progress == TaskProgress.Completed).ToList();

            return this;
        }

        private TasksViewModel SetWorkersDictionary(IEnumerable<User> users)
        {
            users.ToList().ForEach(u => WorkersDictionary.TryAdd(u.Id, u.UserName));

            return this;
        }

        #endregion
    }
}
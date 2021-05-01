using System;
using System.Collections.Generic;
using System.Linq;
using Worktop.Core.Enums;
using Worktop.Models.Domain;

namespace Worktop.ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        public List<TaskItem> UnassignedTasks { get; private set; }
        public List<TaskItem> AssignedTasks { get; private set; }
        public List<TaskItem> InProgressTasks { get; private set; }
        public List<TaskItem> CompletedTasks { get; private set; }
        public List<TaskItem> NotCompletedTasks { get; private set; } = new List<TaskItem>();

        public TasksViewModel(IEnumerable<TaskItem> tasks)
        {
            Title = "Tasks";

            FilterTasks(tasks.ToList());
        }

        #region private

        private void FilterTasks(List<TaskItem> tasks)
        {
            tasks.ForEach(t =>
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
        }

        #endregion
    }
}
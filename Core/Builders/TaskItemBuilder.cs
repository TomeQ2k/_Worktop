using System;
using Worktop.Core.Builders.Interfaces;
using Worktop.Core.Enums;
using Worktop.Models.Domain;

namespace Worktop.Core.Builders
{
    public class TaskItemBuilder : ITaskItemBuilder
    {
        private readonly TaskItem taskItem = new TaskItem();

        public ITaskItemBuilder SetDescription(string description)
        {
            this.taskItem.Description = description;

            return this;
        }

        public ITaskItemBuilder SetDeadline(DateTime deadline)
        {
            this.taskItem.DateDeadline = deadline;

            return this;
        }

        public ITaskItemBuilder WithExecutor(int? executorId = null)
        {
            this.taskItem.ExecutorId = executorId;

            return this;
        }

        public ITaskItemBuilder SetProgress(TaskProgress progress = TaskProgress.Unassigned)
        {
            this.taskItem.Progress = progress;

            return this;
        }

        public TaskItem Build() => this.taskItem;
    }
}
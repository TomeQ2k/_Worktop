using System;
using Worktop.Core.Enums;
using Worktop.Models.Domain;

namespace Worktop.Core.Builders.Interfaces
{
    public interface ITaskItemBuilder : IBuilder<TaskItem>
    {
        ITaskItemBuilder SetDescription(string description);
        ITaskItemBuilder SetDeadline(DateTime deadline);
        ITaskItemBuilder WithExecutor(int? executorId = null);
        ITaskItemBuilder SetProgress(TaskProgress progress = TaskProgress.Unassigned);
    }
}
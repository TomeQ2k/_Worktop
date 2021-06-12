using System;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Builders.Interfaces
{
    public interface ITaskItemBuilder : IBuilder<TaskItem>
    {
        ITaskItemBuilder SetDescription(string description);
        ITaskItemBuilder SetDeadline(DateTime deadline);
        ITaskItemBuilder WithExecutor(int? executorId = null);
        ITaskItemBuilder SetProgress(TaskProgress progress = TaskProgress.Unassigned);
    }
}
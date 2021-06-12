using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels.Partials
{
    public class TaskCardViewModel
    {
        public TaskItem TaskItem { get; set; }
        public bool ReadOnly { get; private set; }

        public static TaskCardViewModel Build(TaskItem taskItem, bool readOnly = false)
            => new TaskCardViewModel { TaskItem = taskItem, ReadOnly = readOnly };
    }
}
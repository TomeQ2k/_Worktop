using System;
using System.ComponentModel.DataAnnotations.Schema;
using Worktop.Core.Enums;

namespace Worktop.Models.Domain
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateArranged { get; set; } = DateTime.Now;
        public DateTime DateDeadline { get; set; }
        public TaskProgress Progress { get; set; }
        public int? ExecutorId { get; set; }

        [NotMapped]
        public bool NotCompleted { get; set; }

        public User Executor { get; set; }
    }
}
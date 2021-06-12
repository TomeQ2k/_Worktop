using System.Collections.Generic;

namespace Worktop.Core.Domain.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Salary { get; set; }

        public ICollection<User> Users { get; set; } = new HashSet<User>();

        public static Job Create(string title, decimal salary) => new Job
        {
            Title = title,
            Salary = salary
        };
    }
}
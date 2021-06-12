using System;

namespace Worktop.Core.Domain.Entities
{
    public class Opinion
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsNegative { get; set; }
        public int SalaryBonusPercentage { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
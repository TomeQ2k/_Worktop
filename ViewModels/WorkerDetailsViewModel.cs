using System;
using System.Collections.Generic;
using System.Linq;
using Worktop.Core.Validators;
using Worktop.Models.Domain;

namespace Worktop.ViewModels
{
    public class WorkerDetailsViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string JobTitle { get; set; }
        public float BaseSalary { get; set; }
        public float TotalSalary { get; set; }
        public DateTime? DateHired { get; set; }

        [RequiredValidator]
        public float SalaryBonus { get; set; }

        [RequiredValidator]
        public int JobId { get; set; }

        public List<Job> Jobs { get; set; }

        public WorkerDetailsViewModel()
        {
            Title = "Worker";
        }

        public WorkerDetailsViewModel WithJobs(IEnumerable<Job> jobs)
        {
            Jobs = jobs.ToList();
            return this;
        }
    }
}
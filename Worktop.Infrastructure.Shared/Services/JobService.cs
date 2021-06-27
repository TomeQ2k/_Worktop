using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Application.Services;
using Worktop.Core.Domain.Data;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Shared.Services
{
    public class JobService : IJobService
    {
        private readonly IDatabase database;
        private readonly IFileReader fileReader;

        private const string JobsFilePath = "/data/jobs.json";

        public JobService(IDatabase database, IFileReader fileReader)
        {
            this.database = database;
            this.fileReader = fileReader;
        }

        public async Task<bool> InsertJobsFromFile()
        {
            var jsonJobs = await fileReader.ReadFile(JobsFilePath);
            var jobs = jsonJobs.FromJSON<IEnumerable<Job>>();
            var jobsFromDatabase = await database.JobRepository.GetAll();

            if (!jobsFromDatabase.Any())
                database.JobRepository.AddRange(jobs);
            else
            {
                foreach (var jobToInsert in jobs)
                    if (!jobsFromDatabase.Any(j => j.Title.ToLower().Equals(jobToInsert.Title.ToLower())))
                        database.JobRepository.Add(jobToInsert);

                var jobsToDelete = jobsFromDatabase.Where(job => !jobs.Any(j => j.Title.ToLower().Equals(job.Title.ToLower())));
                database.JobRepository.DeleteRange(jobsToDelete);
            }

            return await database.Complete();
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Extensions;
using Worktop.Core.Services.Interfaces;
using Worktop.Data;
using Worktop.Models.Domain;

namespace Worktop.Core.Services
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
            var jsonJobs = await fileReader.ReadFile($"{fileReader.WebRootPath}{JobsFilePath}");
            var jobs = jsonJobs.FromJSON<IEnumerable<Job>>();

            if ((await database.JobRepository.Find(j => j != null) == null))
                database.JobRepository.AddRange(jobs);

            return await database.Complete();
        }
    }
}
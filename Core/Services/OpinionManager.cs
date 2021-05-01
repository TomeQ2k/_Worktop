using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Worktop.Data;
using Worktop.Core.Extensions;
using Worktop.Core.Services.Interfaces;
using Worktop.Models.Domain;

namespace Worktop.Core.Services
{
    public class OpinionManager : IOpinionManager
    {
        private readonly IDatabase database;

        private readonly int currentUserId;

        public OpinionManager(IDatabase database, IHttpContextAccessor httpContextAccessor)
        {
            this.database = database;

            this.currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
        }

        public async Task<IEnumerable<Opinion>> FetchOpinions()
        => (await database.OpinionRepository.Filter(o => o.UserId == currentUserId)).OrderByDescending(o => o.DateCreated);

        public async Task<bool> SendOpinion(Opinion opinion)
        {
            var worker = await database.UserRepository.Get(opinion.UserId);

            if (worker == null)
                return false;

            float salaryBonusToAdd = (float)opinion.SalaryBonusPercentage / 100;
            worker.SalaryBonus += !opinion.IsNegative ? salaryBonusToAdd : -salaryBonusToAdd;

            database.OpinionRepository.Add(opinion);
            database.UserRepository.Update(worker);

            return await database.Complete();
        }
    }
}
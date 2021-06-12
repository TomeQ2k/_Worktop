using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Application.Services;
using Worktop.Core.Domain.Data;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Shared.Services
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
            => await database.OpinionRepository.GetUserOpinions(currentUserId);

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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Worktop.Core.Domain.Data.Repositories;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Persistence.Database.Repositories
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Article>> FetchOrderedArticles()
            => await context.Articles.OrderByDescending(a => a.DateUpdated).ToListAsync();
    }
}
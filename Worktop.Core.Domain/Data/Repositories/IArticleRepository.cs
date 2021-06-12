using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Domain.Data.Repositories
{
    public interface IArticleRepository : IRepository<Article>
    {
        Task<IEnumerable<Article>> FetchOrderedArticles();
    }
}
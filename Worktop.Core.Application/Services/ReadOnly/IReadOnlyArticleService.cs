using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyArticleService
    {
        Task<Article> GetArticle(int articleId);
        Task<IEnumerable<Article>> FetchArticles();
    }
}
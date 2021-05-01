using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Models.Domain;

namespace Worktop.Core.Services.Interfaces
{
    public interface IArticleService
    {
        Task<Article> GetArticle(int articleId);
        Task<IEnumerable<Article>> FetchArticles();

        Task<bool> PublishArticle(string title, string content);
        Task<bool> UpdateArticle(int articleId, string title, string content);
        Task<bool> DeleteArticle(int articleId);
    }
}
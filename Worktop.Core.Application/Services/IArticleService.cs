using System.Threading.Tasks;
using Worktop.Core.Application.Services.ReadOnly;

namespace Worktop.Core.Application.Services
{
    public interface IArticleService : IReadOnlyArticleService
    {
        Task<bool> PublishArticle(string title, string content);
        Task<bool> UpdateArticle(int articleId, string title, string content);
        Task<bool> DeleteArticle(int articleId);
    }
}
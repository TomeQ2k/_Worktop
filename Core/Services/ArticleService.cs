using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Data;
using Worktop.Core.Services.Interfaces;
using Worktop.Models.Domain;

namespace Worktop.Core.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IDatabase database;

        public ArticleService(IDatabase database)
        {
            this.database = database;
        }

        public async Task<Article> GetArticle(int articleId) => await database.ArticleRepository.Get(articleId);

        public async Task<IEnumerable<Article>> FetchArticles()
            => (await database.ArticleRepository.Fetch()).OrderByDescending(a => a.DateUpdated);

        public async Task<bool> PublishArticle(string title, string content)
        {
            var article = Article.Create(title, content);

            database.ArticleRepository.Add(article);

            return await database.Complete();
        }

        public async Task<bool> UpdateArticle(int articleId, string title, string content)
        {
            var article = await database.ArticleRepository.Get(articleId);

            if (article == null)
                return false;

            article.Title = title;
            article.Content = content;
            article.DateUpdated = DateTime.Now;

            database.ArticleRepository.Update(article);

            return await database.Complete();
        }

        public async Task<bool> DeleteArticle(int articleId)
        {
            var article = await database.ArticleRepository.Get(articleId);

            if (article == null)
                return false;

            database.ArticleRepository.Delete(article);

            return await database.Complete();
        }
    }
}

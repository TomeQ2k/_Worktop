using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Core.Application.Services;
using Worktop.Core.Domain.Data;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Shared.Services
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
            => await database.ArticleRepository.FetchOrderedArticles();

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

using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels.Partials
{
    public class ArticleCardViewModel
    {
        public Article Article { get; set; }

        public static ArticleCardViewModel Build(Article article) => new ArticleCardViewModel { Article = article };
    }
}
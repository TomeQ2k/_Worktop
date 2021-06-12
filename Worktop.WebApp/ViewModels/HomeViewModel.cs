using System.Collections.Generic;
using System.Linq;
using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public List<Article> Articles { get; set; }

        public HomeViewModel(IEnumerable<Article> articles)
        {
            Title = "Dashboard";

            Articles = articles.ToList();
        }
    }
}
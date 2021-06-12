using System.Diagnostics;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Application.Services;
using Worktop.Core.Common.Helpers;
using Worktop.WebApp.ViewModels;

namespace Worktop.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService articleService;
        private readonly IMapper mapper;

        public HomeController(IArticleService articleService, IMapper mapper)
        {
            this.articleService = articleService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(new HomeViewModel(await articleService.FetchArticles()).WithAlert());

        [HttpGet]
        [Authorize(Policy = Constants.AdminPolicy)]
        public IActionResult NewArticle() => View(new EditArticleViewModel());

        [HttpGet]
        [Authorize(Policy = Constants.AdminPolicy)]
        public async Task<IActionResult> EditArticle(int id)
        {
            var article = await articleService.GetArticle(id);

            return article != null ? (IActionResult)View(mapper.Map<EditArticleViewModel>(article)) : this.ErrorPage();
        }

        [HttpPost]
        [Authorize(Policy = Constants.AdminPolicy)]
        public async Task<IActionResult> PublishArticle(EditArticleViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("NewArticle", viewModel);

            return await articleService.PublishArticle(viewModel.ArticleTitle, viewModel.Content)
                ? (IActionResult)RedirectToAction("Index").PushAlert("Article has been published")
                : View(viewModel.WithAlert("Publishing article failed"));
        }

        [HttpPost]
        [Authorize(Policy = Constants.AdminPolicy)]
        public async Task<IActionResult> UpdateArticle(EditArticleViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("EditArticle", viewModel);

            return await articleService.UpdateArticle(viewModel.Id, viewModel.ArticleTitle, viewModel.Content)
                ? (IActionResult)RedirectToAction("Index").PushAlert("Article has been updated")
                : View(viewModel.WithAlert("Updating article failed"));
        }

        [HttpPost]
        [Authorize(Policy = Constants.AdminPolicy)]
        public async Task<IActionResult> DeleteArticle(int id)
            => await articleService.DeleteArticle(id)
                ? RedirectToAction("Index") : this.ErrorPage();

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
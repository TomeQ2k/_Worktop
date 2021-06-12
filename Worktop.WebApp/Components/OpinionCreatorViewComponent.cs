using Microsoft.AspNetCore.Mvc;
using Worktop.WebApp.ViewModels.Components;

namespace Worktop.WebApp.Components
{
    public class OpinionCreatorViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(OpinionCreatorViewModel viewModel) => View(viewModel);
    }
}
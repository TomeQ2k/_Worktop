using Microsoft.AspNetCore.Mvc;
using Worktop.WebApp.ViewModels.Components;

namespace Worktop.WebApp.Components
{
    public class ChangeEmailViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ChangeEmailCallbackViewModel viewModel) => View(viewModel);
    }
}
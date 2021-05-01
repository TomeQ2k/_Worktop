using Microsoft.AspNetCore.Mvc;
using Worktop.ViewModels.Components;

namespace Worktop.Components
{
    public class ChangeEmailViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ChangeEmailCallbackViewModel viewModel) => View(viewModel);
    }
}
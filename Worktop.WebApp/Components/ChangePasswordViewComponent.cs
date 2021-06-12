using Microsoft.AspNetCore.Mvc;
using Worktop.WebApp.ViewModels.Components;

namespace Worktop.WebApp.Components
{
    public class ChangePasswordViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ChangePasswordViewModel viewModel) => View(viewModel);
    }
}
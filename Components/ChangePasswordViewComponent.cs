using Microsoft.AspNetCore.Mvc;
using Worktop.ViewModels.Components;

namespace Worktop.Components
{
    public class ChangePasswordViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ChangePasswordViewModel viewModel) => View(viewModel);
    }
}
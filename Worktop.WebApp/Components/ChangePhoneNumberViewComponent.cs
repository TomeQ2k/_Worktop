using Microsoft.AspNetCore.Mvc;
using Worktop.WebApp.ViewModels.Components;

namespace Worktop.WebApp.Components
{
    public class ChangePhoneNumberViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ChangePhoneNumberViewModel viewModel) => View(viewModel);
    }
}
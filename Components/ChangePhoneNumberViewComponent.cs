using Microsoft.AspNetCore.Mvc;
using Worktop.ViewModels.Components;

namespace Worktop.Components
{
    public class ChangePhoneNumberViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(ChangePhoneNumberViewModel viewModel) => View(viewModel);
    }
}
using Microsoft.AspNetCore.Mvc;
using Worktop.WebApp.ViewModels.Components;

namespace Worktop.WebApp.Components
{
    public class UploadFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(UploadFormViewModel viewModel) => View(viewModel);
    }
}
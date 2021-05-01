using Microsoft.AspNetCore.Mvc;
using Worktop.ViewModels.Components;

namespace Worktop.Components
{
    public class UploadFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(UploadFormViewModel viewModel) => View(viewModel);
    }
}
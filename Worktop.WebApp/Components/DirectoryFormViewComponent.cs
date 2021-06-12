using Microsoft.AspNetCore.Mvc;
using Worktop.WebApp.ViewModels.Components;

namespace Worktop.WebApp.Components
{
    public class DirectoryFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(DirectoryFormViewModel viewModel) => View(viewModel);
    }
}
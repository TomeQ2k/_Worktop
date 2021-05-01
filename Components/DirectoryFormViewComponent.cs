using Microsoft.AspNetCore.Mvc;
using Worktop.ViewModels.Components;

namespace Worktop.Components
{
    public class DirectoryFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(DirectoryFormViewModel viewModel) => View(viewModel);
    }
}
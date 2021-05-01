using Microsoft.AspNetCore.Mvc;
using Worktop.ViewModels.Components;

namespace Worktop.Components
{
    public class OpinionCreatorViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(OpinionCreatorViewModel viewModel) => View(viewModel);
    }
}
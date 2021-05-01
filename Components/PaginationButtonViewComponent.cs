using Microsoft.AspNetCore.Mvc;
using Worktop.Core.Helpers;

namespace Worktop.Components
{
    public class PaginationButtonViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string actionName, int pageNumber)
        {
            ViewData[Constants.ActionName] = actionName;
            ViewData[Constants.PageNumber] = pageNumber;

            return View();
        }
    }
}
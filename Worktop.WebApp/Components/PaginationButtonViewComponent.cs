using Microsoft.AspNetCore.Mvc;
using Worktop.Core.Common.Helpers;

namespace Worktop.WebApp.Components
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
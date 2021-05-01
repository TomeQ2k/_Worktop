using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Worktop.ViewModels.Components;

namespace Worktop.Components
{
    public class ErrorViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(dynamic key = null, string error = null,
            KeyValuePair<dynamic, string> value = default(KeyValuePair<dynamic, string>))
        {
            ViewBag.Key = key;
            ViewBag.Error = error;

            return View(ErrorComponentViewModel.Build(error: value));
        }
    }
}
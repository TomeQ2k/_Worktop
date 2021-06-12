using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Worktop.WebApp.ViewModels.Components;

namespace Worktop.WebApp.Components
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
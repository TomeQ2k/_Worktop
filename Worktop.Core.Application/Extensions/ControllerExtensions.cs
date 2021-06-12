using Microsoft.AspNetCore.Mvc;
using Worktop.Core.Application.Models.Alert;
using Worktop.Core.Common.Enums;

namespace Worktop.Core.Application.Extensions
{
    public static class ControllerExtensions
    {
        public static RedirectToActionResult ErrorPage(this Controller controller) => controller.RedirectToAction("Error", "Home");

        public static RedirectToActionResult AccessDeniedPage(this Controller controller) => controller.RedirectToAction("AccessDenied", "Auth");

        public static IActionResult PushAlert(this IActionResult view, string message, AlertType type = AlertType.Info)
        {
            Alertify.Push(message, type);
            return view;
        }
    }
}
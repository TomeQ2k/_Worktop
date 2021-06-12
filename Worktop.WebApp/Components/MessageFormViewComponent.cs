using Microsoft.AspNetCore.Mvc;
using Worktop.Core.Domain.Entities;

namespace Worktop.WebApp.Components
{
    public class MessageFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Message message) => View(message);
    }
}
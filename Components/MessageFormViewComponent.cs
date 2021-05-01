using Microsoft.AspNetCore.Mvc;
using Worktop.Models.Domain;

namespace Worktop.Components
{
    public class MessageFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Message message) => View(message);
    }
}
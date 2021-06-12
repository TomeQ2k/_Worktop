using Microsoft.AspNetCore.Mvc;

namespace Worktop.WebApp.Components
{
    public class StorageSizeProgressViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(bool isPrivateStorage = false) => View(isPrivateStorage);
    }
}
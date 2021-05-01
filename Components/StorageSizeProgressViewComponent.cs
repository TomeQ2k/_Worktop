using Microsoft.AspNetCore.Mvc;

namespace Worktop.Components
{
    public class StorageSizeProgressViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(bool isPrivateStorage = false) => View(isPrivateStorage);
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Application.Services;
using Worktop.Core.Common.Enums;

namespace Worktop.WebApp.Components
{
    public class AdminPanelNavbarViewComponent : ViewComponent
    {
        private readonly IRolesService rolesService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AdminPanelNavbarViewComponent(IRolesService rolesService, IHttpContextAccessor httpContextAccessor)
        {
            this.rolesService = rolesService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
            => View(await rolesService.IsPermitted(RoleName.Admin, httpContextAccessor.HttpContext.GetCurrentUserId()));
    }
}
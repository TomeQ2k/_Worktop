using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Worktop.Core.Services.Interfaces;
using Worktop.Models.Domain;
using Worktop.Core.Extensions;

namespace Worktop.Core.Services
{
    public class CookieClaimsPrincipalManager : ICookieClaimsPrincipalManager
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CookieClaimsPrincipalManager(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task SignInWithClaims(User user, bool isPersistent = true, IEnumerable<Claim> customClaims = null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            }
            .AddRoleClaim(user.UserRoles);

            if (customClaims != null)
                claims.AddRange(customClaims);

            var identity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
            var principal = new ClaimsPrincipal(identity);

            await httpContextAccessor.HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal, new AuthenticationProperties { IsPersistent = isPersistent });
        }
    }
}
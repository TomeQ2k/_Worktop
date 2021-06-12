using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
{
    public interface ICookieClaimsPrincipalManager
    {
        Task SignInWithClaims(User user, bool isPersistent = true, IEnumerable<Claim> customClaims = null);
    }
}
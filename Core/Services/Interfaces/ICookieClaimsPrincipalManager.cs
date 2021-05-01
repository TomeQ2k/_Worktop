using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using Worktop.Models.Domain;

namespace Worktop.Core.Services.Interfaces
{
    public interface ICookieClaimsPrincipalManager
    {
        Task SignInWithClaims(User user, bool isPersistent = true, IEnumerable<Claim> customClaims = null);
    }
}
using System.Security.Claims;
using System.Collections.Generic;
using Worktop.Models.Domain;
using System.Linq;
using Worktop.Core.Helpers;

namespace Worktop.Core.Extensions
{
    public static class ClaimsExtensions
    {
        public static List<Claim> AddRoleClaim(this List<Claim> claims, IEnumerable<UserRole> userRoles)
        {
            if (userRoles.Any(ur => ur.Role.Name == Constants.AdminRole))
                claims.Add(new Claim(ClaimTypes.Role, Constants.AdminRole));

            return claims;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Worktop.Core.Common.Helpers;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Extensions
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
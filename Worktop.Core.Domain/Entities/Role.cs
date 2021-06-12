using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Worktop.Core.Common.Enums;
using Worktop.Core.Common.Helpers;

namespace Worktop.Core.Domain.Entities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

        public static Role Create(RoleName roleName) => new Role { Name = Utils.EnumToString(roleName) };
    }
}
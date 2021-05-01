using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Worktop.Core.Enums;
using Worktop.Core.Helpers;

namespace Worktop.Models.Domain
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

        public static Role Create(RoleName roleName) => new Role { Name = Utils.EnumToString(roleName) };
    }
}
using Microsoft.AspNetCore.Identity;

namespace Worktop.Core.Domain.Entities
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }

        public static UserRole Create(int userId, int roleId) => new UserRole { UserId = userId, RoleId = roleId };
    }
}
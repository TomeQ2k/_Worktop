using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Worktop.Data;
using Worktop.Core.Enums;
using Worktop.Core.Services.Interfaces;
using Worktop.Models.Domain;
using Worktop.Core.Helpers;

namespace Worktop.Core.Services
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<User> userManager;
        private readonly IDatabase database;

        public RolesService(RoleManager<Role> roleManager, UserManager<User> userManager, IDatabase database)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.database = database;
        }

        public async Task<bool> AdmitRole(RoleName roleName, User user)
            => user == null ? false : (await userManager.AddToRoleAsync(user, Utils.EnumToString(roleName))).Succeeded;

        public async Task<bool> RevokeRole(RoleName roleName, User user)
            => user == null ? false : (await userManager.RemoveFromRoleAsync(user, Utils.EnumToString(roleName))).Succeeded;

        public async Task<bool> CreateRole(RoleName roleName)
        {
            if (await roleManager.RoleExistsAsync(Utils.EnumToString(roleName)))
                return false;

            var role = Role.Create(roleName);

            return (await roleManager.CreateAsync(role)).Succeeded;
        }

        public async Task<bool> IsPermitted(RoleName roleName, int userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            return user == null ? false : await userManager.IsInRoleAsync(user, Utils.EnumToString(roleName));
        }

        public bool IsPermitted(RoleName roleName, User user)
            => user.UserRoles.Any(ur => ur.Role.Name == Utils.EnumToString(roleName));
    }
}
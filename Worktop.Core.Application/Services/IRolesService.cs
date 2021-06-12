using System.Threading.Tasks;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
{
    public interface IRolesService
    {
        Task<bool> AdmitRole(RoleName roleName, User user);
        Task<bool> RevokeRole(RoleName roleName, User user);

        Task<bool> CreateRole(RoleName roleName);

        Task<bool> IsPermitted(RoleName roleName, int userId);
        bool IsPermitted(RoleName roleName, User user);
    }
}
using System.Threading.Tasks;
using Worktop.Core.Application.Services.ReadOnly;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services
{
    public interface IRolesService : IReadOnlyRolesService
    {
        Task<bool> AdmitRole(RoleName roleName, User user);
        Task<bool> RevokeRole(RoleName roleName, User user);

        Task<bool> CreateRole(RoleName roleName);
    }
}
using System.Threading.Tasks;
using Worktop.Core.Common.Enums;
using Worktop.Core.Domain.Entities;

namespace Worktop.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyRolesService
    {
        Task<bool> IsPermitted(RoleName roleName, int userId);
        bool IsPermitted(RoleName roleName, User user);
    }
}
using Worktop.Core.Application.Services.ReadOnly;

namespace Worktop.Core.Application.Services
{
    public interface IStorageSizeManager : IReadOnlyStorageSizeManager
    {
        string ConvertUnits(uint size);
    }
}
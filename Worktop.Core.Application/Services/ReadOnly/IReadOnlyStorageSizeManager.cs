using System.Threading.Tasks;

namespace Worktop.Core.Application.Services.ReadOnly
{
    public interface IReadOnlyStorageSizeManager
    {
        uint MaxPublicStorageSizeInGb { get; }
        uint MaxPrivateStorageSizeInGb { get; }

        Task<uint> CountStorageSize(bool isPrivateStorage = false);

        uint GetUnitConversionMultiplier();
    }
}
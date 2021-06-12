using System.Threading.Tasks;

namespace Worktop.Core.Application.Services
{
    public interface IStorageSizeManager
    {
        uint MaxPublicStorageSizeInGb { get; }
        uint MaxPrivateStorageSizeInGb { get; }

        Task<uint> CountStorageSize(bool isPrivateStorage = false);
        string ConvertUnits(uint size);

        uint GetUnitConversionMultiplier();
    }
}
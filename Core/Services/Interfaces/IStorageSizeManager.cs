using System.Threading.Tasks;

namespace Worktop.Core.Services.Interfaces
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
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Worktop.Data;
using Worktop.Core.Services.Interfaces;
using System.Threading.Tasks;
using Worktop.Core.Helpers;

namespace Worktop.Core.Services
{
    public class StorageSizeManager : IStorageSizeManager
    {
        private readonly IDatabase database;

        public IConfiguration Configuration { get; }

        public uint MaxPublicStorageSizeInGb { get; }
        public uint MaxPrivateStorageSizeInGb { get; }

        public static readonly uint UnitConversionMultiplier = 1024;

        public StorageSizeManager(IConfiguration configuration, IDatabase database)
        {
            this.database = database;
            Configuration = configuration;

            MaxPublicStorageSizeInGb = Configuration.GetValue<uint>(AppSettingsKeys.MaxPublicStorageSizeInGb);
            MaxPrivateStorageSizeInGb = Configuration.GetValue<uint>(AppSettingsKeys.MaxPrivateStorageSizeInGb);
        }

        public async Task<uint> CountStorageSize(bool isPrivateStorage = false)
            => !isPrivateStorage
            ? (uint)(await database.FileRepository.Filter(f => f.UserId == null)).Sum(f => f.Size)
            : (uint)(await database.FileRepository.Filter(f => f.UserId != null)).Sum(f => f.Size);

        public string ConvertUnits(uint size)
        {
            if (size < UnitConversionMultiplier)
                return $"{size} KB";
            else if (size < Math.Pow(UnitConversionMultiplier, 2))
                return $"{Math.Round((double)size / UnitConversionMultiplier, 2)} MB";
            else if (size < Math.Pow(UnitConversionMultiplier, 3))
                return $"{Math.Round((double)size / Math.Pow(UnitConversionMultiplier, 2), 3)} GB";
            else
                return $"{size} KB";
        }

        public uint GetUnitConversionMultiplier() => UnitConversionMultiplier;
    }
}
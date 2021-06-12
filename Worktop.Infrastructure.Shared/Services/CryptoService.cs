using System.Data;
using System.Security.Cryptography;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Worktop.Core.Application.Services;
using Worktop.Core.Common.Helpers;

namespace Worktop.Infrastructure.Shared.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly IDataProtectionProvider dataProtectionProvider;

        public string ProtectorToken { get; }

        public IConfiguration Configuration { get; }

        public CryptoService(IDataProtectionProvider dataProtectionProvider, IConfiguration configuration)
        {
            this.dataProtectionProvider = dataProtectionProvider;

            Configuration = configuration;
            ProtectorToken = Configuration.GetValue<string>(AppSettingsKeys.TLSToken);
        }

        public string Encrypt(string plainText)
        {
            var dataProtector = dataProtectionProvider.CreateProtector(ProtectorToken);

            return dataProtector.Protect(plainText);
        }

        public string Decrypt(string cipherText)
        {
            try
            {
                var dataProtector = dataProtectionProvider.CreateProtector(ProtectorToken);

                return dataProtector.Unprotect(cipherText);
            }
            catch (CryptographicException)
            {
                throw new DataException();
            }
        }
    }
}
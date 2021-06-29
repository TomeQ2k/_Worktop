using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Worktop.Core.Application.Helpers;
using Worktop.Core.Application.Services;
using Worktop.Core.Common.Helpers;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Shared.Services
{
    public class ResetPasswordManager : IResetPasswordManager
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailSender emailSender;
        private readonly ICryptoService cryptoService;

        public IConfiguration Configuration { get; }

        public ResetPasswordManager(UserManager<User> userManager, IEmailSender emailSender, ICryptoService cryptoService, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.cryptoService = cryptoService;

            this.Configuration = configuration;
        }

        public async Task<bool> ResetPassword(string email, string newPassword, string token)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
                return false;

            token = cryptoService.Decrypt(token);
            newPassword = cryptoService.Decrypt(newPassword);

            return (await userManager.ResetPasswordAsync(user, token, newPassword)).Succeeded;
        }

        public async Task<bool> SendResetPasswordCallback(string email, string newPassword)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
                return false;

            var resetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);
            resetPasswordToken = cryptoService.Encrypt(resetPasswordToken);

            newPassword = cryptoService.Encrypt(newPassword);

            string callbackUrl = $"{Configuration.GetValue<string>(AppSettingsKeys.ServerAddress)}/Auth/ConfirmResetPassword?email={user.Email}&newPassword={newPassword}&token={resetPasswordToken}";

            return await emailSender.Send(EmailMessages.ResetPasswordEmail(email, callbackUrl));
        }
    }
}
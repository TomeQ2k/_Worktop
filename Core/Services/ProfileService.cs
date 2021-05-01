using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Worktop.Data;
using Worktop.Core.Extensions;
using Worktop.Core.Services.Interfaces;
using Worktop.Models.Domain;
using Worktop.Core.Helpers;
using Worktop.Core.Enums;

namespace Worktop.Core.Services
{
    public class ProfileService : IProfileService, ICanExecute<string>
    {
        private readonly UserManager<User> userManager;
        private readonly ICryptoService cryptoService;
        private readonly IEmailSender emailSender;
        private readonly IAuthService authService;
        private readonly IDatabase database;

        public IConfiguration Configuration { get; }

        private readonly int currentUserId;

        public ProfileService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, ICryptoService cryptoService, IEmailSender emailSender,
            IConfiguration configuration, IAuthService authService, IDatabase database)
        {
            this.userManager = userManager;
            this.cryptoService = cryptoService;
            this.emailSender = emailSender;
            this.authService = authService;
            this.database = database;

            Configuration = configuration;

            this.currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
        }

        public async Task<User> GetCurrentUser() => await database.UserRepository.GetUserWithJob(currentUserId);

        public async Task<bool> ChangePassword(string oldPassword, string newPassword)
        {
            var currentUser = await GetCurrentUser();

            return currentUser == null ? false : (await userManager.ChangePasswordAsync(currentUser, oldPassword, newPassword)).Succeeded;
        }

        public async Task<bool> ChangeEmail(string newEmail, string token)
        {
            if (!await CanExecute(newEmail))
                return false;

            var currentUser = await GetCurrentUser();

            if (currentUser == null)
                return false;

            token = cryptoService.Decrypt(token);

            return (await userManager.ChangeEmailAsync(currentUser, newEmail, token)).Succeeded;
        }

        public async Task<bool> SendChangeEmailCallback(string newEmail)
        {
            if (!await CanExecute(newEmail))
                return false;

            var currentUser = await GetCurrentUser();

            if (currentUser == null)
                return false;

            var changeEmailToken = await userManager.GenerateChangeEmailTokenAsync(currentUser, newEmail);
            changeEmailToken = cryptoService.Encrypt(changeEmailToken);

            string callbackUrl = $"{Configuration.GetValue<string>(AppSettingsKeys.ServerAddress)}Profile/ConfirmChangeEmail?newEmail={newEmail}&token={changeEmailToken}";

            return await emailSender.Send(Constants.EmailChangeEmail(newEmail, callbackUrl));
        }

        public async Task<bool> ChangePhoneNumber(string phoneNumber)
        {
            var currentUser = await GetCurrentUser();

            if (currentUser == null)
                return false;

            var changePhoneNumberToken = await userManager.GenerateChangePhoneNumberTokenAsync(currentUser, phoneNumber);

            return (await userManager.ChangePhoneNumberAsync(currentUser, phoneNumber, changePhoneNumberToken)).Succeeded;
        }

        public async Task<bool> CanExecute(string item)
        {
            if (await authService.EmailExists(item))
            {
                Alertify.Push("Email address already exists", AlertType.Error);
                return false;
            }

            return true;
        }
    }
}
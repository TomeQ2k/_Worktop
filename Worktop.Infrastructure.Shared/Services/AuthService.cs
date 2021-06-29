using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Worktop.Core.Application.Helpers;
using Worktop.Core.Application.Models.Alert;
using Worktop.Core.Application.Services;
using Worktop.Core.Common.Enums;
using Worktop.Core.Common.Helpers;
using Worktop.Core.Domain.Data;
using Worktop.Core.Domain.Entities;

namespace Worktop.Infrastructure.Shared.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailSender emailSender;
        private readonly IRolesService rolesService;
        private readonly ICryptoService cryptoService;
        private readonly IDatabase database;

        public IConfiguration Configuration { get; }

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender,
            IRolesService rolesService,
            IConfiguration configuration, ICryptoService cryptoService, IDatabase database)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.rolesService = rolesService;
            this.cryptoService = cryptoService;
            this.database = database;

            this.Configuration = configuration;
        }

        public async Task<User> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Alertify.Push("Invalid email address or password", AlertType.Error);
                return null;
            }

            var user = await database.UserRepository.FindUserToLogin(email);

            if (user == null)
            {
                Alertify.Push("Invalid email address or password", AlertType.Error);
                return null;
            }

            if ((await signInManager.PasswordSignInAsync(user, password, true, false)).Succeeded)
            {
                if (!user.EmailConfirmed)
                {
                    Alertify.Push("Account is not confirmed", AlertType.Warning);
                    return null;
                }

                return user;
            }

            Alertify.Push("Invalid email address or password", AlertType.Error);
            return null;
        }

        public async Task<User> Register(string email, string username, string password)
        {
            var user = User.Create(email, username);

            if (await EmailExists(email))
            {
                Alertify.Push("Email address already exists", AlertType.Error);
                return null;
            }

            if (await UsernameExists(username))
            {
                Alertify.Push("Username already exists", AlertType.Error);
                return null;
            }

            if ((await userManager.CreateAsync(user, password)).Succeeded)
            {
                await rolesService.AdmitRole(RoleName.User, user);

                var confirmAccountToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
                confirmAccountToken = cryptoService.Encrypt(confirmAccountToken);

                string callbackUrl =
                    $"{Configuration.GetValue<string>(AppSettingsKeys.ServerAddress)}/Auth/ConfirmAccount?email={user.Email}&token={confirmAccountToken}";

                await emailSender.Send(EmailMessages.ActivationAccountEmail(email, callbackUrl));

                return user;
            }

            Alertify.Push("Creating account failed", AlertType.Error);
            return null;
        }

        public async Task<bool> ConfirmAccount(string email, string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            token = cryptoService.Decrypt(token);

            var user = await userManager.FindByEmailAsync(email);

            return user == null ? false : (await userManager.ConfirmEmailAsync(user, token)).Succeeded;
        }

        public async Task Logout() => await signInManager.SignOutAsync();

        public async Task<bool> EmailExists(string email) => await userManager.FindByEmailAsync(email) != null;

        public async Task<bool> UsernameExists(string username) => await userManager.FindByNameAsync(username) != null;
    }
}
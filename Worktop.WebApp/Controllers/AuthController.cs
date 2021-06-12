using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Worktop.Core.Application.Extensions;
using Worktop.Core.Application.Filters;
using Worktop.Core.Application.Services;
using Worktop.WebApp.ViewModels;

namespace Worktop.WebApp.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        private readonly IResetPasswordManager resetPasswordManager;
        private readonly ICookieClaimsPrincipalManager cookieClaimsPrincipalManager;

        public AuthController(IAuthService authService, IResetPasswordManager resetPasswordManager, ICookieClaimsPrincipalManager cookieClaimsPrincipalManager)
        {
            this.authService = authService;
            this.resetPasswordManager = resetPasswordManager;
            this.cookieClaimsPrincipalManager = cookieClaimsPrincipalManager;
        }

        [HttpGet]
        [ServiceFilter(typeof(OnlyAnonymousFilter))]
        public IActionResult Login() => View(new LoginViewModel().WithAlert());

        [HttpGet]
        [ServiceFilter(typeof(OnlyAnonymousFilter))]
        public IActionResult Register() => View(new RegisterViewModel().WithAlert());

        [HttpGet]
        [ServiceFilter(typeof(OnlyAnonymousFilter))]
        public IActionResult ResetPassword() => View(new ResetPasswordViewModel().WithAlert());

        [HttpGet]
        public IActionResult AccessDenied() => View();

        [HttpPost]
        [ServiceFilter(typeof(OnlyAnonymousFilter))]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            var loggedUser = await authService.Login(viewModel.Email, viewModel.Password);

            if (loggedUser != null)
            {
                await cookieClaimsPrincipalManager.SignInWithClaims(loggedUser);

                return RedirectToAction("Index", "Home");
            }

            return View(viewModel.WithError());
        }

        [HttpPost]
        [ServiceFilter(typeof(OnlyAnonymousFilter))]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var createdUser = await authService.Register(viewModel.Email, viewModel.UserName, viewModel.Password);

            if (createdUser != null)
                return RedirectToAction("Login").PushAlert("User was registered. You have to confirm your account");

            return View(viewModel.WithError());
        }

        [HttpGet]
        [ServiceFilter(typeof(OnlyAnonymousFilter))]
        public async Task<IActionResult> ConfirmAccount([FromQuery] ConfirmAccountViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return this.ErrorPage();

            return await authService.ConfirmAccount(viewModel.Email, viewModel.Token)
                ? (IActionResult)RedirectToAction("Login").PushAlert("Account was confirmed") : this.ErrorPage();
        }

        [HttpPost]
        [ServiceFilter(typeof(OnlyAnonymousFilter))]
        public async Task<IActionResult> SendResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("ResetPassword", viewModel);

            await resetPasswordManager.SendResetPasswordCallback(viewModel.Email, viewModel.NewPassword);

            return RedirectToAction("ResetPassword").PushAlert($"Reset password token was sent to: {viewModel.Email}");
        }

        [HttpGet]
        [ServiceFilter(typeof(OnlyAnonymousFilter))]
        public async Task<IActionResult> ConfirmResetPassword([FromQuery] ResetPasswordCallbackViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return this.ErrorPage();

            return await resetPasswordManager.ResetPassword(viewModel.Email, viewModel.NewPassword, viewModel.Token)
                ? (IActionResult)RedirectToAction("Login").PushAlert("Password has been changed") : this.ErrorPage();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await authService.Logout();

            return RedirectToAction("Login");
        }
    }
}
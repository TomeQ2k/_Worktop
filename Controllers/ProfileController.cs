using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Worktop.Core.Extensions;
using Worktop.Core.Services.Interfaces;
using Worktop.Models.Domain;
using Worktop.ViewModels;
using Worktop.ViewModels.Components;

namespace Worktop.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;
        private readonly IMapper mapper;

        private static ProfileViewModel profileViewModel;

        public ProfileController(IProfileService profileService, IMapper mapper)
        {
            this.profileService = profileService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUser = await profileService.GetCurrentUser();

            if (currentUser == null)
                return this.AccessDeniedPage();

            profileViewModel = (ProfileViewModel)mapper.Map<User, ProfileViewModel>(currentUser, profileViewModel).WithAlert();

            return View(profileViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Index", profileViewModel);

            return RedirectToAction("Index").PushAlert(await profileService.ChangePassword(viewModel.OldPassword, viewModel.NewPassword)
                    ? "Password has been changed" : "Changing password failed. Check if old password is valid");
        }

        [HttpPost]
        public async Task<IActionResult> SendChangeEmail(ChangeEmailCallbackViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Index", profileViewModel);

            var changeEmailTokenSent = await profileService.SendChangeEmailCallback(viewModel.NewEmail);

            return changeEmailTokenSent
                ? (IActionResult)RedirectToAction("Index").PushAlert(changeEmailTokenSent ? $"Change email token was sent to: {viewModel.NewEmail}" : "Sending change email token failed")
                : View("Index", profileViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmChangeEmail([FromQuery] ChangeEmailViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return this.ErrorPage();

            var emailChanged = await profileService.ChangeEmail(viewModel.NewEmail, viewModel.Token);

            return emailChanged ? (IActionResult)View("Index").PushAlert("Email has been changed") : this.ErrorPage();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePhoneNumber(ChangePhoneNumberViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Index", profileViewModel);

            return await profileService.ChangePhoneNumber(viewModel.PhoneNumber)
                ? (IActionResult)RedirectToAction("Index").PushAlert("Phone number has been changed")
                : this.ErrorPage();
        }
    }
}
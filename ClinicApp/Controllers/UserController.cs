using ClinicApp.Models.Dto;
using ClinicApp.Services.User;
using ClinicApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IIdentityUserService _identityUserService;

        public UserController(IIdentityUserService identityUserService)
        {
            _identityUserService = identityUserService;
        }

        public IActionResult Login()
        {
            if (_identityUserService.IsSignedIn(User))
                return RedirectToAction(nameof(HomeController.Index), "Home");

            return View(new UserCredentialsDto());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserCredentialsDto credentials)
        {
            if (!ModelState.IsValid) return View(credentials);

            if ((await _identityUserService.SignIn(credentials.UserName, credentials.Password)).Succeeded)
                return RedirectToAction(nameof(HomeController.Index), "Home");

            ModelState.AddModelError("", "Invalid credentials. Try again.");
            return View(credentials);
        }

        public IActionResult Register()
        {
            return View(new RegistrationViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityUserService.RegisterPatient(registrationViewModel.UserName, registrationViewModel.Password);

                if (result == RegistrationResult.UserExists)
                    ModelState.AddModelError("UserName", "A user with the specified name already exists.");
                else return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View(registrationViewModel);
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _identityUserService.SignOut();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}

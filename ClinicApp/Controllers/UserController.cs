using ClinicApp.Models.Dto;
using ClinicApp.Models.Users;
using ClinicApp.Services.User;
using ClinicApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IIdentityAuthenticationService _authenticationService;
        private readonly IRegistrationService _registrationService;

        public UserController(IIdentityAuthenticationService authenticationService, IRegistrationService registrationService)
        {
            _authenticationService = authenticationService;
            _registrationService = registrationService;
        }

        public IActionResult Login()
        {
            if (_authenticationService.IsSignedIn(User))
                return RedirectToAction(nameof(HomeController.Index), "Home");

            return View(new UserCredentialsDto());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserCredentialsDto credentials)
        {
            if (!ModelState.IsValid) return View(credentials);

            if ((await _authenticationService.SignIn(credentials.UserName, credentials.Password)).Succeeded)
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
                if (registrationViewModel.Password != registrationViewModel.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View(registrationViewModel);
                }

                var patient = new Patient
                {
                    UserName = registrationViewModel.UserName, 
                    FirstName = registrationViewModel.FirstName,
                    LastName = registrationViewModel.LastName
                };

                var result = await _registrationService.RegisterAsync(patient, registrationViewModel.Password);

                if (result.Succeeded)
                    return RedirectToAction(nameof(HomeController.Index), "Home");

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(registrationViewModel);
        }

        [Authorize, HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.SignOut();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}

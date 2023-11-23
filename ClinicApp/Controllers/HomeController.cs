using ClinicApp.Models.Dto;
using ClinicApp.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IIdentityUserService _identityUserService;

        public HomeController(IIdentityUserService identityUserService)
        {
            _identityUserService = identityUserService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View(new UserCredentialsDto());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserCredentialsDto credentials)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _identityUserService.SignIn(credentials.UserName, credentials.Password);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("UserName", "Invalid credentials.");
                    return RedirectToAction(nameof(Login));
                }
            }

            return View(credentials);
        }

        public IActionResult Register()
        {
            return View(new UserCredentialsDto());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserCredentialsDto credentials)
        {
            if (ModelState.IsValid)
            {
                var result = await _identityUserService.RegisterPatient(credentials.UserName, credentials.Password);

                if (result == RegistrationResult.UserExists)
                    ModelState.AddModelError("UserName", "A user with the specified name already exists.");
                else return RedirectToAction(nameof(Index));
            }

            return View(credentials);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _identityUserService.SignOut();
            return RedirectToAction(nameof(Index));
        }
    }
}

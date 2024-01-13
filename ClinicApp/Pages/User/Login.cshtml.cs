using ClinicApp.Models.Dto;
using ClinicApp.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicApp.Pages.User
{
    public class LoginModel(IIdentityAuthenticationService authenticationService) : PageModel
    {
        [BindProperty] public UserCredentialsDto UserCredentials { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await authenticationService.SignIn(UserCredentials.UserName, UserCredentials.Password);

                if (result.Succeeded)
                    return Redirect("/app");

                ModelState.AddModelError("", "Invalid credentials. Try again.");
            }

            return Page();
        }
    }
}

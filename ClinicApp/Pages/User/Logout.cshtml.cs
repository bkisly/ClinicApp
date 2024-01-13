using ClinicApp.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClinicApp.Pages.User
{
    public class LogoutModel(IIdentityAuthenticationService authenticationService) : PageModel
    {
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            authenticationService.SignOut();
            return Redirect("/app");
        }
    }
}

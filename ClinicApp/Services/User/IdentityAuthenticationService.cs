using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public class IdentityAuthenticationService(SignInManager<IdentityUser> signInManager) : IIdentityAuthenticationService
    {
        public async Task<SignInResult> SignIn(string userName, string password) 
            => await signInManager.PasswordSignInAsync(userName, password, false, false);

        public async Task SignOut() => await signInManager.SignOutAsync();
        public bool IsSignedIn(ClaimsPrincipal user) => signInManager.IsSignedIn(user);
    }
}

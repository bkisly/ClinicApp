using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public class IdentityAuthenticationService : IIdentityAuthenticationService
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public IdentityAuthenticationService(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> SignIn(string userName, string password) 
            => await _signInManager.PasswordSignInAsync(userName, password, false, false);

        public async Task SignOut() => await _signInManager.SignOutAsync();
        public bool IsSignedIn(ClaimsPrincipal user) => _signInManager.IsSignedIn(user);
    }
}

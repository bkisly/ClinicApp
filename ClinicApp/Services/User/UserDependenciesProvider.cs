using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public class UserDependenciesProvider<TUser> : IUserDependenciesProvider<TUser> where TUser : IdentityUser
    {
        public UserManager<TUser> UserManager { get; }
        public SignInManager<TUser> SignInManager { get; }

        public UserDependenciesProvider(UserManager<TUser> userManager, SignInManager<TUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public interface IUserDependenciesProvider<TUser> where TUser : IdentityUser
    {
        UserManager<TUser> UserManager { get; }
        SignInManager<TUser> SignInManager { get; }
    }
}

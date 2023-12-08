using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public interface IUserDependenciesProvider
    {
        UserManager<IdentityUser> DefaultManager { get; }
        UserManager<TUser> ProvideManager<TUser>() where TUser : IdentityUser, new();
        string? ProvideRoleName<TUser>() where TUser : IdentityUser, new();
    }
}

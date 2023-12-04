using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public interface IUserDependenciesProvider
    {
        UserManager<IdentityUser> DefaultManager { get; }
        UserManager<TUser> ProvideManager<TUser>(TUser user) where TUser : IdentityUser;
        string? ProvideRoleName<TUser>(TUser user) where TUser : IdentityUser;
    }
}

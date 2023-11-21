using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public interface IUserManagerProvider
    {
        UserManager<TUser> Provide<TUser>(TUser user) where TUser : IdentityUser;
    }
}

using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public interface IIdentityUserService : IUserService
    {
        Task SignOut(IdentityUser user);
    }
}

using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public interface IIdentityUserService : IUserService
    {
        Task SignOut<TUser>(TUser user) where TUser : IdentityUser;
    }
}

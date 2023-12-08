using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public interface IRegistrationService
    {
        Task<IdentityResult> RegisterAsync<TUser>(TUser user, string password) where TUser : IdentityUser, new();
    }
}

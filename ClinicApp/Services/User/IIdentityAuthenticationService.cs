using System.Security.Claims;

namespace ClinicApp.Services.User
{
    public interface IIdentityAuthenticationService : IAuthenticationService
    {
        Task SignOut();
        bool IsSignedIn(ClaimsPrincipal user);
    }
}

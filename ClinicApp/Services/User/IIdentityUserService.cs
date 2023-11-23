using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public interface IIdentityUserService : IUserService
    {
        Task SignOut();
        bool IsSignedIn(ClaimsPrincipal user);
    }
}

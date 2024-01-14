using System.Security.Claims;

namespace ClinicApp.Services.User
{
    public interface IBlazorUserService
    {
        ClaimsPrincipal User { get; }
    }
}

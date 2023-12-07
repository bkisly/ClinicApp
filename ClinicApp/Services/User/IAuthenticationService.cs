using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public interface IAuthenticationService
    {
        Task<SignInResult> SignIn(string userName, string password);
    }
}

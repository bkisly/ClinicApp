using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public interface IRegistrationService
    {
        /*Task<RegistrationResult> RegisterDoctor(string userName, string password, byte specialityId);
        Task<RegistrationResult> RegisterPatient(string userName, string password);
        Task<RegistrationResult> RegisterManager(string userName, string password);*/

        Task<RegistrationResult> RegisterAsync<TUser>(TUser user, string password) where TUser : IdentityUser;
    }

    public enum RegistrationResult
    {
        Succeeded,
        UserExists,
    }
}

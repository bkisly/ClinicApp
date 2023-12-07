using System.Security.Claims;

namespace ClinicApp.Services.User
{
    public interface IUserService
    {
        Task ActivatePatient(string patientId);
        Task<string?> GetRoleForUser(ClaimsPrincipal user);
    }
}

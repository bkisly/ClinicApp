using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public interface IUserService
    {
        Task ActivatePatient(string patientId);
        Task<string?> GetRoleForUser(ClaimsPrincipal user);
    }
}

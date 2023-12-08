using System.Security.Claims;
using ClinicApp.Models.Users;

namespace ClinicApp.Services.User
{
    public class UserService(IUserDependenciesProvider userDependenciesProvider) : IUserService
    {
        public async Task ActivatePatient(string patientId)
        {
            var mgr = userDependenciesProvider.ProvideManager<Patient>();
            var patient = await mgr.FindByIdAsync(patientId);

            if (patient != null)
            {
                patient.IsActivated = true;
                await mgr.UpdateAsync(patient);
            }
            else throw new NullReferenceException($"Patient with ID {patientId} not found.");
        }

        public async Task<string?> GetRoleForUser(ClaimsPrincipal user)
        {
            var mgr = userDependenciesProvider.DefaultManager;
            var id = mgr.GetUserId(user);

            if (id == null)
                return null;

            var identityUser = await mgr.FindByIdAsync(id);

            return identityUser == null ? null : (await mgr.GetRolesAsync(identityUser)).FirstOrDefault();
        }
    }
}

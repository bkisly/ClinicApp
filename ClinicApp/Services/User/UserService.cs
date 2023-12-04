using ClinicApp.Models.Users;

namespace ClinicApp.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserDependenciesProvider _userDependenciesProvider;

        public UserService(IUserDependenciesProvider userDependenciesProvider)
        {
            _userDependenciesProvider = userDependenciesProvider;
        }

        public async Task ActivatePatient(string patientId)
        {
            var mgr = _userDependenciesProvider.ProvideManager(new Patient());
            var patient = await mgr.FindByIdAsync(patientId);

            if (patient != null)
            {
                patient.IsActivated = true;
                await mgr.UpdateAsync(patient);
            }
            else throw new NullReferenceException($"Patient with ID {patientId} not found.");
        }
    }
}

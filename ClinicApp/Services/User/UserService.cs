using ClinicApp.Models.Users;

namespace ClinicApp.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserManagerProvider _userManagerProvider;

        public UserService(IUserManagerProvider userManagerProvider)
        {
            _userManagerProvider = userManagerProvider;
        }

        public async Task ActivatePatient(string patientId)
        {
            var mgr = _userManagerProvider.Provide(new Patient());
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

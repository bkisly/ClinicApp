namespace ClinicApp.Services.User
{
    public interface IUserService
    {
        Task<RegistrationResult> RegisterDoctor(string userName, string password, byte specialityId);
        Task<RegistrationResult> RegisterPatient(string userName, string password);
        Task<RegistrationResult> RegisterManager(string userName, string password);
        Task SignIn(string userName, string password);
    }

    public enum RegistrationResult
    {
        Succeeded,
        UserExists,
    }
}

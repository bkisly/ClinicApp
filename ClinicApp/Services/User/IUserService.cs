using ClinicApp.Infrastructure;

namespace ClinicApp.Services.User
{
    public interface IUserService
    {
        Task RegisterDoctor(string userName, string password, byte specialityId);
        Task RegisterPatient(string userName, string password);
        Task RegisterManager(string userName, string password);
        Task SignIn(string userName, string password);
    }
}

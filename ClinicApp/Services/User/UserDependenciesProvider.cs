using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public class UserDependenciesProvider(UserManager<IdentityUser> defaultUserManager,
            UserManager<Manager> managerUserManager,
            UserManager<Doctor> doctorUserManager, UserManager<Patient> patientUserManager)
        : IUserDependenciesProvider
    {
        public UserManager<IdentityUser> DefaultManager { get; } = defaultUserManager;

        public UserManager<TUser> ProvideManager<TUser>() where TUser : IdentityUser, new()
            => (UserManager<TUser>)GetManager(new TUser());

        public string? ProvideRoleName<TUser>() where TUser : IdentityUser, new() => new TUser() switch
        {
            Manager => Constants.Roles.ManagerRoleName,
            Doctor => Constants.Roles.DoctorRoleName,
            Patient => Constants.Roles.PatientRoleName,
            _ => null
        };

        private object GetManager<TUser>(TUser user) => user switch
        {
            Manager => managerUserManager,
            Doctor => doctorUserManager,
            Patient => patientUserManager,
            _ => DefaultManager
        };
    }
}

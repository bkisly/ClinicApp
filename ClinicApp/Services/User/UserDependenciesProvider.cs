using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public class UserDependenciesProvider : IUserDependenciesProvider
    {
        private readonly UserManager<Manager> _managerUserManager;
        private readonly UserManager<Doctor> _doctorUserManager;
        private readonly UserManager<Patient> _patientUserManager;

        public UserManager<IdentityUser> DefaultManager { get; }

        public UserDependenciesProvider(UserManager<IdentityUser> defaultUserManager, UserManager<Manager> managerUserManager, 
            UserManager<Doctor> doctorUserManager, UserManager<Patient> patientUserManager)
        {
            DefaultManager = defaultUserManager;
            _managerUserManager = managerUserManager;
            _doctorUserManager = doctorUserManager;
            _patientUserManager = patientUserManager;
        }

        public UserManager<TUser> ProvideManager<TUser>(TUser user) where TUser : IdentityUser
            => (UserManager<TUser>)GetManager(user);

        public string? ProvideRoleName<TUser>(TUser user) where TUser : IdentityUser => user switch
        {
            Manager => Constants.Roles.ManagerRoleName,
            Doctor => Constants.Roles.DoctorRoleName,
            Patient => Constants.Roles.PatientRoleName,
            _ => null
        };

        private object GetManager<TUser>(TUser user) => user switch
        {
            Manager => _managerUserManager,
            Doctor => _doctorUserManager,
            Patient => _patientUserManager,
            _ => DefaultManager
        };
    }
}

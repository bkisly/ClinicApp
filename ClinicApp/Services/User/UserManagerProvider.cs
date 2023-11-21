using ClinicApp.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public class UserManagerProvider : IUserManagerProvider
    {
        private readonly UserManager<Manager> _managerUserManager;
        private readonly UserManager<Doctor> _doctorUserManager;
        private readonly UserManager<Patient> _patientUserManager;

        public UserManager<IdentityUser> DefaultManager { get; }

        public UserManagerProvider(UserManager<IdentityUser> defaultUserManager, UserManager<Manager> managerUserManager, 
            UserManager<Doctor> doctorUserManager, UserManager<Patient> patientUserManager)
        {
            DefaultManager = defaultUserManager;
            _managerUserManager = managerUserManager;
            _doctorUserManager = doctorUserManager;
            _patientUserManager = patientUserManager;
        }

        public UserManager<TUser> Provide<TUser>(TUser user) where TUser : IdentityUser
            => (UserManager<TUser>)GetManager(user);

        private object GetManager<TUser>(TUser user) => user switch
        {
            Manager => _managerUserManager,
            Doctor => _doctorUserManager,
            Patient => _patientUserManager,
            _ => DefaultManager
        };
    }
}

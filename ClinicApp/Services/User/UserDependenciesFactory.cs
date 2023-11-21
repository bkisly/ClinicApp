using ClinicApp.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public class UserDependenciesFactory : IUserDependenciesFactory
    {
        public IUserDependenciesProvider<IdentityUser> DefaultProvider { get; }
        public IUserDependenciesProvider<Manager> ManagerProvider { get; }
        public IUserDependenciesProvider<Doctor> DoctorProvider { get; }
        public IUserDependenciesProvider<Patient> PatientProvider { get; }

        public UserDependenciesFactory(IUserDependenciesProvider<IdentityUser> defaultProvider, IUserDependenciesProvider<Manager> managerProvider,
            IUserDependenciesProvider<Doctor> doctorProvider, IUserDependenciesProvider<Patient> patientProvider)
        {
            DefaultProvider = defaultProvider;
            ManagerProvider = managerProvider;
            DoctorProvider = doctorProvider;
            PatientProvider = patientProvider;
        }
    }
}

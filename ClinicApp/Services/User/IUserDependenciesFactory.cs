using ClinicApp.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace ClinicApp.Services.User
{
    public interface IUserDependenciesFactory
    {
        IUserDependenciesProvider<IdentityUser> DefaultProvider { get; }
        IUserDependenciesProvider<Manager> ManagerProvider { get; }
        IUserDependenciesProvider<Doctor> DoctorProvider { get; }
        IUserDependenciesProvider<Patient> PatientProvider { get; }
    }
}

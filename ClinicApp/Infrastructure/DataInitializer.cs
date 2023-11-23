using ClinicApp.Models;
using ClinicApp.Models.Users;
using ClinicApp.Repositories;
using ClinicApp.Services.User;
using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Infrastructure
{
    public static class DataInitializer
    {
        public static void PopulateSpecialities(ISpecialityRepository specialityRepository)
        {
            if (specialityRepository.Specialities.ToList().Any())
                return;

            foreach (var speciality in GetSpecialities())
            {
                specialityRepository.AddSpeciality(speciality);
            }
        }

        public static async Task EnsureRoles(RoleManager<IdentityRole> roleManager)
        {
            var managerRole = new IdentityRole(Constants.Roles.ManagerRoleName);
            var doctorRole = new IdentityRole(Constants.Roles.DoctorRoleName);
            var patientRole = new IdentityRole(Constants.Roles.PatientRoleName);

            await roleManager.CreateAsync(managerRole);
            await roleManager.CreateAsync(doctorRole);
            await roleManager.CreateAsync(patientRole);
        }

        public static async Task CreateManagerAccount(IRegistrationService registrationService, string managerUserName, string managerPassword)
        {
            await registrationService.RegisterManager(managerUserName, managerPassword);
        }

        public static IEnumerable<Speciality> GetSpecialities() => new[]
        {
            new Speciality { Id = 1, Name = "GP" },
            new Speciality { Id = 2, Name = "Laryngologist" },
            new Speciality { Id = 3, Name = "Dermatologist" },
            new Speciality { Id = 4, Name = "Opthalmologist" },
            new Speciality { Id = 5, Name = "Neurologist" },
            new Speciality { Id = 6, Name = "Orthopedist" },
            new Speciality { Id = 7, Name = "Pediatrist" },
        };
    }
}

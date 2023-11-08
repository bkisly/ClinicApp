using ClinicApp.Data;
using ClinicApp.Models;
using ClinicApp.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public static IEnumerable<Speciality> GetSpecialities() => new[]
        {
            new Speciality { Name = "GP" },
            new Speciality { Name = "Laryngologist" },
            new Speciality { Name = "Dermatologist" },
            new Speciality { Name = "Opthalmologist" },
            new Speciality { Name = "Neurologist" },
            new Speciality { Name = "Orthopedist" },
            new Speciality { Name = "Pediatrist" },
        };
    }
}

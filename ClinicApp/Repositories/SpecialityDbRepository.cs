using ClinicApp.Data;
using ClinicApp.Models;

namespace ClinicApp.Repositories
{
    public class SpecialityDbRepository(ApplicationDbContext context) : ISpecialityRepository
    {
        public IQueryable<Speciality> Specialities => context.Specialities;

        public Speciality GetById(byte id) => context.Specialities.Single(s => s.Id == id);

        public void AddSpeciality(Speciality speciality)
        {
            context.Specialities.Add(speciality);
            context.SaveChanges();
        }
    }
}

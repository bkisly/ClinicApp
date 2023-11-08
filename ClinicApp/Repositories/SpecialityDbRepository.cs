using ClinicApp.Data;
using ClinicApp.Models;

namespace ClinicApp.Repositories
{
    public class SpecialityDbRepository : ISpecialityRepository
    {
        private readonly ApplicationDbContext _context;

        public IQueryable<Speciality> Specialities => _context.Specialities;

        public SpecialityDbRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddSpeciality(Speciality speciality)
        {
            _context.Specialities.Add(speciality);
            _context.SaveChanges();
        }
    }
}

using ClinicApp.Models;

namespace ClinicApp.Repositories
{
    public interface ISpecialityRepository
    {
        IQueryable<Speciality> Specialities { get; }
        void AddSpeciality(Speciality speciality);
    }
}

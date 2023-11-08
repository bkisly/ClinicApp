using ClinicApp.Models;

namespace ClinicApp.Repositories
{
    public interface ISpecialityRepository
    {
        IQueryable Specialities { get; }
        void AddSpeciality(Speciality speciality);
    }
}

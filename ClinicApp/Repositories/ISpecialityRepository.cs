using ClinicApp.Models;

namespace ClinicApp.Repositories
{
    public interface ISpecialityRepository
    {
        IQueryable<Speciality> Specialities { get; }

        Speciality GetById(byte id);
        void AddSpeciality(Speciality speciality);
    }
}

using ClinicApp.Models;

namespace ClinicApp.Services.Visit
{
    public interface IVisitService
    {
        IEnumerable<DateTime> GetAvailableDatesForScheduleEntry(ScheduleEntry entry);
        Task<IList<Models.Visit>> FindByPatientId(string patientId);
        Task<IList<Models.Visit>> FindByDoctorId(string doctorId);
        Task AddAsync(Models.Visit visit);
        Task UpdateAsync(int visitId, Models.Visit entity);
        Task<Models.Visit?> FindByIdAsync(int id);
    }
}
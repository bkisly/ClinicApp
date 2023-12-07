using ClinicApp.Models;

namespace ClinicApp.Services.Schedule
{
    public interface IScheduleService
    {
        IEnumerable<ScheduleEntry> GetEntriesByWeek(int week, string doctorId);
        Task CopyPreviousWeek(int previousWeek, string doctorId);
        Task<IEnumerable<ScheduleEntry>> GetEntriesByDoctor(string doctorId);

        Task<ScheduleEntry> GetByIdAsync(int id);
        Task AddAsync(ScheduleEntry scheduleEntry);
        Task UpdateAsync(int id, ScheduleEntry scheduleEntry);
        Task DeleteAsync(int id);
    }
}

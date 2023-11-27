using ClinicApp.Models;

namespace ClinicApp.Repositories
{
    public interface IScheduleEntryRepository
    {
        IQueryable<ScheduleEntry> ScheduleEntries { get; }

        Task<ScheduleEntry> GetByIdAsync(int id);
        Task AddAsync(ScheduleEntry scheduleEntry);
        Task UpdateAsync(int id, ScheduleEntry scheduleEntry);
        Task DeleteAsync(int id);
    }
}

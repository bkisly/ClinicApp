using ClinicApp.Models;

namespace ClinicApp.Services.Schedule
{
    public interface IScheduleService
    {
        IEnumerable<ScheduleEntry> GetEntriesByWeek(int week);
        Task CopyPreviousWeek(int previousWeek);
    }
}

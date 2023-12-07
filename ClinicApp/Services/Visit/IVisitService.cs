using ClinicApp.Models;

namespace ClinicApp.Services.Visit
{
    public interface IVisitService
    {
        IEnumerable<DateTime> GetAvailableDatesForScheduleEntry(ScheduleEntry entry);
    }
}

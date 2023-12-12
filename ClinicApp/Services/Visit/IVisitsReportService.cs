using ClinicApp.Models;

namespace ClinicApp.Services.Visit
{
    public interface IVisitsReportService
    {
        int GetVisitsCountForScheduleEntry(ScheduleEntry entry);
    }
}

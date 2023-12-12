using ClinicApp.Models;
using ClinicApp.Repositories;

namespace ClinicApp.Services.Visit
{
    public class VisitsReportService(IVisitRepository visitRepository) : IVisitsReportService
    {
        public int GetVisitsCountForScheduleEntry(ScheduleEntry entry)
        {
            return visitRepository.Visits.Count(v => v.Date.Date == entry.Date.ToDateTime(new TimeOnly()) && v.VisitStatusId == (byte)VisitStatusEnum.Finished);
        }
    }
}

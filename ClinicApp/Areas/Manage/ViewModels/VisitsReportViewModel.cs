using ClinicApp.Models;

namespace ClinicApp.Areas.Manage.ViewModels
{
    public class VisitsReportViewModel
    {
        public Dictionary<ScheduleEntry, int> EntriesToVisitsCount { get; set; } = null!;
        public string DoctorId { get; set; } = null!;
        public DateOnly SelectedDate { get; set; }
    }
}

using ClinicApp.Models;

namespace ClinicApp.Areas.Manage.ViewModels
{
    public class ScheduleEntriesViewModel
    {
        public int WeekNumber { get; set; }
        public string DoctorId { get; set; } = string.Empty;
        public IEnumerable<ScheduleEntry> ScheduleEntries { get; set; } = null!;
    }
}

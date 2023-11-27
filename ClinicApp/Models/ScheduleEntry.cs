using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using ClinicApp.Models.ValidationAttributes;

namespace ClinicApp.Models
{
    public class ScheduleEntry
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        [MinutesDividibleBy(Constants.VisitDurationMinutes)] public TimeOnly Begin { get; set; }
        [MinutesDividibleBy(Constants.VisitDurationMinutes), EndLaterThanBegin] public TimeOnly End { get; set; }

        public Doctor Doctor { get; set; } = null!;
    }
}

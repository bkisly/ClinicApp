using ClinicApp.Infrastructure;
using ClinicApp.Models;
using ClinicApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Services.Visit
{
    public class VisitService(IVisitRepository visitRepository) : IVisitService
    {
        public IEnumerable<DateTime> GetAvailableDatesForScheduleEntry(ScheduleEntry entry)
        {
            var allDates = new List<DateTime>();
            var takenDates = visitRepository.Visits
                .Include(v => v.Doctor)
                .Where(v => v.Doctor.Id == entry.DoctorId)
                .Where(v => DateOnly.FromDateTime(v.Date) == entry.Date)
                .Where(v => TimeOnly.FromDateTime(v.Date) >= entry.Begin)
                .Where(v => TimeOnly.FromDateTime(v.Date) < entry.End)
                .Select(v => v.Date)
                .ToArray();

            for (var beginDateTime = new DateTime(entry.Date, entry.Begin);
                 beginDateTime < new DateTime(entry.Date, entry.End);
                 beginDateTime += TimeSpan.FromMinutes(Constants.VisitDurationMinutes))
            {
                allDates.Add(beginDateTime);
            }

            return allDates.Except(takenDates.AsEnumerable());
        }
    }
}

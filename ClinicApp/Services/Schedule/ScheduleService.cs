using ClinicApp.Infrastructure;
using ClinicApp.Models;
using ClinicApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Services.Schedule
{
    public class ScheduleService(IScheduleEntryRepository repository) : IScheduleService
    {
        public IEnumerable<ScheduleEntry> GetEntriesByWeek(int week)
            => repository.ScheduleEntries
                .Include(entry => entry.Doctor)
                .Where(entry => entry.Date.WeekNumber() == week)
                .ToArray();

        public async Task CopyPreviousWeek(int previousWeek)
        {
            var entries = GetEntriesByWeek(previousWeek);

            foreach (var entry in entries)
            {
                var newEntry = (ScheduleEntry)entry.Clone();
                newEntry.Date = newEntry.Date.AddDays(7);
                await repository.AddAsync(newEntry);
            }
        }
    }
}

using ClinicApp.Infrastructure;
using ClinicApp.Models;
using ClinicApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Services.Schedule
{
    public class ScheduleService(IScheduleEntryRepository repository) : IScheduleService
    {
        public IEnumerable<ScheduleEntry> GetEntriesByWeek(int week, string doctorId)
        {
            var possibleDates = DateOnlyExtensions.GetDatesForWeek(week);

            return repository.ScheduleEntries
                .Include(entry => entry.Doctor)
                .Where(entry => possibleDates.Contains(entry.Date) && entry.Doctor.Id == doctorId)
                .ToArray();
        }

        public async Task CopyPreviousWeek(int previousWeek, string doctorId)
        {
            var entries = GetEntriesByWeek(previousWeek, doctorId);

            foreach (var entry in entries)
            {
                var newEntry = (ScheduleEntry)entry.Clone();
                newEntry.Date = newEntry.Date.AddDays(7);
                await repository.AddAsync(newEntry);
            }
        }

        public async Task<ScheduleEntry> GetByIdAsync(int id) => await repository.GetByIdAsync(id);

        public async Task AddAsync(ScheduleEntry scheduleEntry) => await repository.AddAsync(scheduleEntry);

        public async Task UpdateAsync(int id, ScheduleEntry scheduleEntry) 
            => await repository.UpdateAsync(id, scheduleEntry);

        public async Task DeleteAsync(int id) => await repository.DeleteAsync(id);
    }
}

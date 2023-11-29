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

                if (IsValid(newEntry))
                    await repository.AddAsync(newEntry);
            }
        }

        public async Task<ScheduleEntry> GetByIdAsync(int id) => await repository.GetByIdAsync(id);

        public async Task AddAsync(ScheduleEntry scheduleEntry)
        {
            if (IsValid(scheduleEntry)) await repository.AddAsync(scheduleEntry);
            else throw new ArgumentException("An entry within the specified time range already exists.");
        }

        public async Task UpdateAsync(int id, ScheduleEntry scheduleEntry)
        {
            if (IsValid(scheduleEntry)) await repository.UpdateAsync(id, scheduleEntry);
            else throw new ArgumentException("An entry within the specified time range already exists.");
        }

        public async Task DeleteAsync(int id) => await repository.DeleteAsync(id);

        private bool IsValid(ScheduleEntry scheduleEntry)
        {
            var doctorId = scheduleEntry.Doctor?.Id ?? scheduleEntry.DoctorId;

            var collidingEntries = repository.ScheduleEntries
                .Include(entry => entry.Doctor)
                .Where(entry => entry.Id != scheduleEntry.Id 
                                && entry.Date == scheduleEntry.Date 
                                && entry.Doctor.Id == doctorId
                                && (scheduleEntry.Begin.IsBetween(entry.Begin, entry.End) ||
                                    scheduleEntry.End.IsBetween(entry.Begin, entry.End)))
                .ToArray();

            return collidingEntries.Length == 0;
        }
    }
}

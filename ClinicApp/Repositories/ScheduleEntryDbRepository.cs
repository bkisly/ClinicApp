using ClinicApp.Data;
using ClinicApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Repositories
{
    public class ScheduleEntryDbRepository(ApplicationDbContext context) : IScheduleEntryRepository
    {
        public IQueryable<ScheduleEntry> ScheduleEntries => context.ScheduleEntries;

        public async Task<ScheduleEntry> GetByIdAsync(int id) => await ScheduleEntries.SingleAsync(entry => entry.Id == id);

        public async Task AddAsync(ScheduleEntry scheduleEntry)
        {
            await context.ScheduleEntries.AddAsync(scheduleEntry);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ScheduleEntry scheduleEntry)
        {
            var entry = await GetByIdAsync(id);
            scheduleEntry.CopyTo(entry);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            context.ScheduleEntries.Remove(await GetByIdAsync(id));
            await context.SaveChangesAsync();
        }
    }
}

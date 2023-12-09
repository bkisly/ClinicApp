using ClinicApp.Data;
using ClinicApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Repositories
{
    public class VisitStatusDbRepository(ApplicationDbContext context) : IVisitStatusRepository
    {
        public IQueryable<VisitStatus> VisitStatus => context.VisitsStatus;

        public async Task<VisitStatus?> FindByIdAsync(byte id) => await context.VisitsStatus.SingleOrDefaultAsync(v => v.Id == id);

        public async Task AddAsync(VisitStatus entity)
        {
            await context.VisitsStatus.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<VisitStatus> entities)
        {
            await context.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }
    }
}

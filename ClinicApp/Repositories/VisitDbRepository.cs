using ClinicApp.Data;
using ClinicApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Repositories
{
    public class VisitDbRepository(ApplicationDbContext context) : IVisitRepository
    {
        public IQueryable<Visit> Visits => context.Visits;
        public async Task<Visit?> FindByIdAsync(int id) => await Visits.SingleOrDefaultAsync(v => v.Id == id);

        public async Task AddAsync(Visit entity)
        {
            await context.Visits.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Visit entity)
        {
            var visitToEdit = await context.Visits.SingleAsync(v => v.Id == id);
            entity.CopyTo(visitToEdit);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var visitToDelete = await context.Visits.SingleAsync(v => v.Id == id);
            context.Visits.Remove(visitToDelete);
            await context.SaveChangesAsync();
        }
    }
}

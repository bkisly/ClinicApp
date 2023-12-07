using ClinicApp.Models;

namespace ClinicApp.Repositories
{
    public interface IVisitRepository
    {
        IQueryable<Visit> Visits { get; }

        Task<Visit?> FindByIdAsync(int id);
        Task AddAsync(Visit entity);
        Task UpdateAsync(int id, Visit entity);
        Task DeleteAsync(int id);
    }
}
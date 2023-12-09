using ClinicApp.Models;

namespace ClinicApp.Repositories
{
    public interface IVisitStatusRepository
    {
        public IQueryable<VisitStatus> VisitStatus { get; }
        public Task<VisitStatus?> FindByIdAsync(byte id);
        public Task AddAsync(VisitStatus entity);
        public Task AddRangeAsync(IEnumerable<VisitStatus> entities);
    }
}

using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>  options) : base(options) { }
    }
}

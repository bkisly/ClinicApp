using ClinicApp.Models;
using ClinicApp.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public virtual DbSet<Speciality> Specialities => Set<Speciality>();
        public virtual DbSet<Doctor> Doctors => Set<Doctor>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>  options) : base(options) { }
    }
}

using ClinicApp.Models;
using ClinicApp.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<Speciality> Specialities => Set<Speciality>();
        public virtual DbSet<Doctor> Doctors => Set<Doctor>();
        public virtual DbSet<Patient> Patients => Set<Patient>();
        public virtual DbSet<Manager> Managers => Set<Manager>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>  options) : base(options) { }
    }
}

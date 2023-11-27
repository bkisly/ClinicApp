using ClinicApp.Models;
using ClinicApp.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public virtual DbSet<Speciality> Specialities => Set<Speciality>();
        public virtual DbSet<ScheduleEntry> ScheduleEntries => Set<ScheduleEntry>();

        public virtual DbSet<Doctor> Doctors => Set<Doctor>();
        public virtual DbSet<Patient> Patients => Set<Patient>();
        public virtual DbSet<Manager> Managers => Set<Manager>();
    }
}

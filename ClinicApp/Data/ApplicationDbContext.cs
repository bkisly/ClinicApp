﻿using ClinicApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Speciality> Specialities => Set<Speciality>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>  options) : base(options) { }
    }
}

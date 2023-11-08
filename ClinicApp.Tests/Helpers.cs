using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Tests
{
    internal static class Helpers
    {
        public static ApplicationDbContext CreateInMemoryContext(string dbName)
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
            return new ApplicationDbContext(contextOptions);
        }
    }
}

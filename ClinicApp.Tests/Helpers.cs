using ClinicApp.Data;
using ClinicApp.Models;
using ClinicApp.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ClinicApp.Tests
{
    internal static class Helpers
    {
        public static ApplicationDbContext CreateInMemoryContext(string dbName)
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
            return new ApplicationDbContext(contextOptions);
        }

        public static Mock<ISpecialityRepository> GetSpecialityRepositoryMock(IList<Speciality> specialities)
        {
            var mock = new Mock<ISpecialityRepository>();

            mock.SetupGet(repository => repository.Specialities).Returns(specialities.AsQueryable());
            mock.Setup(repository => repository.AddSpeciality(It.IsAny<Speciality>()))
                .Callback((Speciality s) => specialities.Add(s));

            return mock;
        }
    }
}

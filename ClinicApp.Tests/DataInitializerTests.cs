using ClinicApp.Models;
using ClinicApp.Repositories;
using Moq;

namespace ClinicApp.Tests
{
    public class DataInitializerTests
    {
        [Fact]
        public void CanAddSpecialitiesToEmptyCollection()
        {
            // Arrange

            var specialities = new List<Speciality>();

            var mock = new Mock<ISpecialityRepository>();
            mock.SetupGet(repository => repository.Specialities).Returns(specialities.AsQueryable());
            mock.Setup(repository => repository.AddSpeciality(It.IsAny<Speciality>()))
                .Callback((Speciality s) => specialities.Add(s));
        }
    }
}

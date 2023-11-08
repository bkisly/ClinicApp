using ClinicApp.Infrastructure;
using ClinicApp.Models;
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
            var mock = Helpers.GetSpecialityRepositoryMock(specialities);

            // Act

            DataInitializer.PopulateSpecialities(mock.Object);

            // Assert

            Assert.Equivalent(DataInitializer.GetSpecialities(), specialities);
            mock.Verify(repository => repository.AddSpeciality(It.IsAny<Speciality>()), Times.Exactly(DataInitializer.GetSpecialities().Count()));
        }

        [Fact]
        public void CannotAddSpecialitiesToNonEmptyCollection()
        {
            // Arrange

            var specialities = new List<Speciality>
            {
                new() { Name = "Spec 1" },
                new() { Name = "Spec 2" },
            };

            var mock = Helpers.GetSpecialityRepositoryMock(specialities);

            // Act

            DataInitializer.PopulateSpecialities(mock.Object);

            // Assert

            Assert.Equal(2, specialities.Count);
            mock.Verify(repository => repository.AddSpeciality(It.IsAny<Speciality>()), Times.Never);
        }
    }
}

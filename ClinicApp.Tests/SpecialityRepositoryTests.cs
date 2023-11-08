using ClinicApp.Models;
using ClinicApp.Repositories;

namespace ClinicApp.Tests
{
    public class SpecialityRepositoryTests
    {
        [Fact]
        public void CanQuerySpecialities()
        {
            // Arrange

            var context = Helpers.CreateInMemoryContext("CanQuerySpeciality_DB");
            var repository = new SpecialityDbRepository(context);
            var speciality1 = new Speciality { Id = 1, Name = "Spec 1" };
            var speciality2 = new Speciality { Id = 2, Name = "Spec 2" };

            context.Specialities.Add(speciality1);
            context.Specialities.Add(speciality2);
            context.SaveChanges();

            // Act

            var specialities = repository.Specialities;

            // Assert

            Assert.Equivalent(new[] { speciality1, speciality2 }, specialities);
        }

        [Fact]
        public void CanAddSpeciality()
        {
            // Arrange

            var context = Helpers.CreateInMemoryContext("CanAddSpeciality_DB");
            var repository = new SpecialityDbRepository(context);
            var speciality1 = new Speciality { Id = 1, Name = "Spec 1" };
            var speciality2 = new Speciality { Id = 2, Name = "Spec 2" };

            // Act

            repository.AddSpeciality(speciality1);
            repository.AddSpeciality(speciality2);

            // Assert

            Assert.Equivalent(new[] { speciality1, speciality2 }, context.Specialities);
        }
    }
}

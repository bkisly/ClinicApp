using System.ComponentModel.DataAnnotations;
using ClinicApp.Models;

namespace ClinicApp.Tests.ModelsValidation
{
    public class SpecialityModelTests
    {
        [Fact]
        public void CanInstantiateValidModel()
        {
            // Arrange

            var speciality = new Speciality { Id = 1, Name = "Spec 1" };
            var validationContext = new ValidationContext(speciality);
            var validationErrors = new List<ValidationResult>();

            // Act

            var result = Validator.TryValidateObject(speciality, validationContext, validationErrors, true);

            // Assert

            Assert.True(result);
            Assert.Empty(validationErrors);
        }

        [Fact]
        public void CannotValidateModelWithTooLongName()
        {
            // Arrange

            var longName = "";
            for (int i = 0; i < 300; i++)
                longName += "A";

            var speciality = new Speciality { Id = 1, Name = longName };
            var validationContext = new ValidationContext(speciality);
            var validationErrors = new List<ValidationResult>();

            // Act

            var result = Validator.TryValidateObject(speciality, validationContext, validationErrors, true);

            // Assert

            Assert.False(result);
            Assert.Single(validationErrors);
            Assert.Equal(nameof(speciality.Name), validationErrors.First().MemberNames.Single());
        }

        [Fact]
        public void CannotValidateModelWithoutRequiredProperties()
        {
            // Arrange

            var speciality = new Speciality();
            var validationContext = new ValidationContext(speciality);
            var validationErrors = new List<ValidationResult>();

            // Act

            var result = Validator.TryValidateObject(speciality, validationContext, validationErrors, true);

            // Assert

            Assert.False(result);
            Assert.Single(validationErrors);
            Assert.Equal(nameof(speciality.Name), validationErrors.First().MemberNames.Single());
        }
    }
}

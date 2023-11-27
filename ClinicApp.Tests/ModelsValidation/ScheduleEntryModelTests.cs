using System.ComponentModel.DataAnnotations;
using ClinicApp.Models;
using ClinicApp.Models.Users;

namespace ClinicApp.Tests.ModelsValidation
{
    public class ScheduleEntryModelTests
    {
        [Fact]
        public void CanInstantiateValidModel()
        {
            // Arrange

            var entry = new ScheduleEntry
            {
                Id = 1, Begin = new TimeOnly(12, 30), 
                End = new TimeOnly(14, 45), 
                Date = new DateOnly(2023, 11, 27),
                Doctor = new Doctor()
            };

            var context = new ValidationContext(entry);
            var errors = new List<ValidationResult>();

            // Act

            var result = Validator.TryValidateObject(entry, context, errors, true);

            // Assert

            Assert.True(result);
            Assert.Empty(errors);
        }

        [Fact]
        public void CannotInstantiateModelWithInvalidBeginMinutes()
        {
            // Arrange

            var entry = new ScheduleEntry
            {
                Id = 1,
                Begin = new TimeOnly(12, 32),
                End = new TimeOnly(14, 45),
                Date = new DateOnly(2023, 11, 27),
                Doctor = new Doctor()
            };

            var context = new ValidationContext(entry);
            var errors = new List<ValidationResult>();

            // Act

            var result = Validator.TryValidateObject(entry, context, errors, true);

            // Assert

            Assert.False(result);
            Assert.Single(errors);
            Assert.Equal(nameof(entry.Begin), errors.Single().MemberNames.Single());
        }

        [Fact]
        public void CannotInstantiateModelWithInvalidEndMinutes()
        {
            // Arrange

            var entry = new ScheduleEntry
            {
                Id = 1,
                Begin = new TimeOnly(12, 30),
                End = new TimeOnly(14, 47),
                Date = new DateOnly(2023, 11, 27),
                Doctor = new Doctor()
            };

            var context = new ValidationContext(entry);
            var errors = new List<ValidationResult>();

            // Act

            var result = Validator.TryValidateObject(entry, context, errors, true);

            // Assert

            Assert.False(result);
            Assert.Single(errors);
            Assert.Equal(nameof(entry.End), errors.Single().MemberNames.Single());
        }

        [Fact]
        public void CannotInstantiateModelWithEndEarlierThanBegin()
        {
            // Arrange

            var entry = new ScheduleEntry
            {
                Id = 1,
                Begin = new TimeOnly(12, 30),
                End = new TimeOnly(11, 45),
                Date = new DateOnly(2023, 11, 27),
                Doctor = new Doctor()
            };

            var context = new ValidationContext(entry);
            var errors = new List<ValidationResult>();

            // Act

            var result = Validator.TryValidateObject(entry, context, errors, true);

            // Assert

            Assert.False(result);
            Assert.Single(errors);
            Assert.Equal(nameof(entry.End), errors.Single().MemberNames.Single());
        }

        [Fact]
        public void CannotInstantiateModelWithEndEqualBegin()
        {
            // Arrange

            var entry = new ScheduleEntry
            {
                Id = 1,
                Begin = new TimeOnly(12, 30),
                End = new TimeOnly(12, 30),
                Date = new DateOnly(2023, 11, 27),
                Doctor = new Doctor()
            };

            var context = new ValidationContext(entry);
            var errors = new List<ValidationResult>();

            // Act

            var result = Validator.TryValidateObject(entry, context, errors, true);

            // Assert

            Assert.False(result);
            Assert.Single(errors);
            Assert.Equal(nameof(entry.End), errors.Single().MemberNames.Single());
        }
    }
}

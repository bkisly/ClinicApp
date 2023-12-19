using ClinicApp.Models;
using ClinicApp.Models.Users;
using ClinicApp.Repositories;
using ClinicApp.Services.Visit;
using Moq;

namespace ClinicApp.Tests
{
    public class VisitServiceTests
    {
        [Fact]
        public void CanGetAvailableDates()
        {
            // Arrange

            var visits = new List<Visit>
            {
                new()
                {
                    Date = new DateTime(2023, 12, 7, 13, 45, 0),
                    Doctor = new Doctor { Id = "abcde" }, VisitStatusId = (byte)VisitStatusEnum.Cancelled
                },
                new()
                {
                    Date = new DateTime(2023, 12, 7, 13, 45, 0),
                    Doctor = new Doctor { Id = "abcdef" }, VisitStatusId = (byte)VisitStatusEnum.SignedUp
                },
                new()
                {
                    Date = new DateTime(2023, 12, 7, 14, 0, 0),
                    Doctor = new Doctor { Id = "abcde" }, VisitStatusId = (byte)VisitStatusEnum.SignedUp
                },
                new()
                {
                    Date = new DateTime(2023, 12, 7, 12, 45, 0),
                    Doctor = new Doctor { Id = "abcde" }, VisitStatusId = (byte)VisitStatusEnum.Finished
                },
                new()
                {
                    Date = new DateTime(2023, 12, 8, 12, 45, 0),
                    Doctor = new Doctor { Id = "abcde" }, VisitStatusId = (byte)VisitStatusEnum.SignedUp
                },
                new()
                {
                    Date = new DateTime(2023, 12, 7, 15, 0, 0),
                    Doctor = new Doctor { Id = "abcde" }, VisitStatusId = (byte)VisitStatusEnum.SignedUp
                },
            };
            var entry = new ScheduleEntry
            {
                Begin = new TimeOnly(12, 45),
                Date = new DateOnly(2023, 12, 7),
                End = new TimeOnly(15, 0),
                DoctorId = "abcde"
            };

            var repositoryMock = new Mock<IVisitRepository>();
            repositoryMock.SetupGet(r => r.Visits).Returns(visits.AsQueryable());
            
            var service = new VisitService(repositoryMock.Object);

            // Act

            var availableDates = service.GetAvailableDatesForScheduleEntry(entry);

            // Assert

            Assert.Equal(7, availableDates.Count());
        }
    }
}

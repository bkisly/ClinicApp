using ClinicApp.Infrastructure;
using ClinicApp.Models;
using ClinicApp.Models.Users;
using ClinicApp.Services.Schedule;
using Moq;

namespace ClinicApp.Tests
{
    public class ScheduleServiceTests
    {
        [Fact]
        public void CanGetEntriesByWeekNumber()
        {
            // Arrange

            var doctor = new Doctor { Id = "abc" };
            var entries = new List<ScheduleEntry>
            {
                new()
                {
                    Begin = new TimeOnly(12, 30), End = new TimeOnly(14, 30), Date = new DateOnly(2023, 11, 27),
                    Doctor = doctor
                },
                new()
                {
                    Begin = new TimeOnly(12, 30), End = new TimeOnly(14, 30), Date = new DateOnly(2023, 11, 28),
                    Doctor = doctor
                },
                new()
                {
                    Begin = new TimeOnly(12, 30), End = new TimeOnly(14, 30), Date = new DateOnly(2023, 11, 26),
                    Doctor = doctor
                },
                new()
                {
                    Begin = new TimeOnly(12, 30), End = new TimeOnly(14, 30), Date = new DateOnly(2023, 11, 25),
                    Doctor = doctor
                },
            };

            var repositoryMock = Helpers.GetScheduleEntryRepositoryMock(entries);
            var service = new ScheduleService(repositoryMock.Object);
            var weekNumber = new DateOnly(2023, 11, 27).WeekNumber();

            // Act

            var filteredEntries = service.GetEntriesByWeek(weekNumber, doctor.Id).ToArray();

            // Assert

            Assert.Equal(2, filteredEntries.Length);
            Assert.All(filteredEntries, entry => Assert.Equal(weekNumber, entry.Date.WeekNumber()));
        }

        [Fact]
        public async Task CanCopyPreviousWeek()
        {
            // Arrange

            var doctor = new Doctor { Id = "abc" };
            var entries = new List<ScheduleEntry>
            {
                new()
                {
                    Begin = new TimeOnly(12, 30), End = new TimeOnly(14, 30), Date = new DateOnly(2023, 11, 26),
                    Doctor = doctor, Id = 1
                },
                new()
                {
                    Begin = new TimeOnly(12, 30), End = new TimeOnly(14, 30), Date = new DateOnly(2023, 11, 25),
                    Doctor = doctor, Id = 2
                },
            };

            var repositoryMock = Helpers.GetScheduleEntryRepositoryMock(entries);
            var service = new ScheduleService(repositoryMock.Object);
            var weekNumber = new DateOnly(2023, 11, 26).WeekNumber();

            // Act

            await service.CopyPreviousWeek(weekNumber, doctor.Id);

            // Assert

            Assert.Equal(4, entries.Count);
            Assert.Equal(weekNumber + 1, entries[2].Date.WeekNumber());
            Assert.Equal(weekNumber + 1, entries[3].Date.WeekNumber());
            Assert.Equal(entries[2].Date, new DateOnly(2023, 12, 3));
            Assert.Equal(entries[3].Date, new DateOnly(2023, 12, 2));
        }

        [Fact]
        public async Task CannotAddCollidingTimeRange()
        {
            // Arrange

            var doctor = new Doctor { Id = "abc" };
            var entries = new List<ScheduleEntry>
            {
                new()
                {
                    Begin = new TimeOnly(12, 30), End = new TimeOnly(14, 30), Date = new DateOnly(2023, 11, 26),
                    Doctor = doctor, Id = 1
                },
                new()
                {
                    Begin = new TimeOnly(12, 30), End = new TimeOnly(14, 30), Date = new DateOnly(2023, 11, 25),
                    Doctor = doctor, Id = 2
                },
            };

            var repositoryMock = Helpers.GetScheduleEntryRepositoryMock(entries);
            var service = new ScheduleService(repositoryMock.Object);

            // Assert

            await Assert.ThrowsAsync<ArgumentException>(async () => await service.AddAsync(new ScheduleEntry
                {
                    Begin = new TimeOnly(12, 30),
                    End = new TimeOnly(14, 30),
                    Date = new DateOnly(2023, 11, 25),
                    Doctor = doctor,
                    Id = 3,
                })
            );
        }

        [Fact]
        public async Task CannotCopyIntoCollidingRange()
        {
            // Arrange

            var doctor = new Doctor { Id = "abc" };
            var entries = new List<ScheduleEntry>
            {
                new()
                {
                    Begin = new TimeOnly(12, 30), End = new TimeOnly(14, 30), Date = new DateOnly(2023, 11, 26),
                    Doctor = doctor, Id = 1
                },
                new()
                {
                    Begin = new TimeOnly(12, 30), End = new TimeOnly(14, 30), Date = new DateOnly(2023, 12, 3),
                    Doctor = doctor, Id = 2
                },
            };

            var repositoryMock = Helpers.GetScheduleEntryRepositoryMock(entries);
            var service = new ScheduleService(repositoryMock.Object);
            var weekNumber = new DateOnly(2023, 11, 26).WeekNumber();

            // Act

            await service.CopyPreviousWeek(weekNumber, doctor.Id);


            // Assert

            Assert.Equal(2, entries.Count);
            repositoryMock.Verify(r => r.AddAsync(It.IsAny<ScheduleEntry>()), Times.Never);
        }
    }
}

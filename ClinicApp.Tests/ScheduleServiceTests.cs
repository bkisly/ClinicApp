using ClinicApp.Infrastructure;
using ClinicApp.Models;
using ClinicApp.Models.Users;
using ClinicApp.Services.Schedule;

namespace ClinicApp.Tests
{
    public class ScheduleServiceTests
    {
        [Fact]
        public void CanGetEntriesByWeekNumber()
        {
            // Arrange

            var entries = new List<ScheduleEntry>
            {
                new()
                {
                    Begin = new TimeOnly(12, 30), End = new TimeOnly(14, 30), Date = new DateOnly(2023, 11, 27),
                    Doctor = new Doctor()
                },
                new()
                {
                    Begin = new TimeOnly(12, 30), End = new TimeOnly(14, 30), Date = new DateOnly(2023, 11, 28),
                    Doctor = new Doctor()
                },
                new()
                {
                    Begin = new TimeOnly(12, 30), End = new TimeOnly(14, 30), Date = new DateOnly(2023, 11, 26),
                    Doctor = new Doctor()
                },
                new()
                {
                    Begin = new TimeOnly(12, 30), End = new TimeOnly(14, 30), Date = new DateOnly(2023, 11, 25),
                    Doctor = new Doctor()
                },
            };

            var repositoryMock = Helpers.GetScheduleEntryRepositoryMock(entries);
            var service = new ScheduleService(repositoryMock.Object);
            var weekNumber = new DateOnly(2023, 11, 27).WeekNumber();

            // Act

            var filteredEntries = service.GetEntriesByWeek(weekNumber).ToArray();

            // Assert

            Assert.Equal(2, filteredEntries.Length);
            Assert.All(filteredEntries, entry => Assert.Equal(weekNumber, entry.Date.WeekNumber()));
        }

        [Fact]
        public async Task CanCopyPreviousWeek()
        {
            // Arrange

            var entries = new List<ScheduleEntry>
            {
                new()
                {
                    Begin = new TimeOnly(12, 30), End = new TimeOnly(14, 30), Date = new DateOnly(2023, 11, 26),
                    Doctor = new Doctor(), Id = 1
                },
                new()
                {
                    Begin = new TimeOnly(12, 30), End = new TimeOnly(14, 30), Date = new DateOnly(2023, 11, 25),
                    Doctor = new Doctor(), Id = 2
                },
            };

            var repositoryMock = Helpers.GetScheduleEntryRepositoryMock(entries);
            var service = new ScheduleService(repositoryMock.Object);
            var weekNumber = new DateOnly(2023, 11, 26).WeekNumber();

            // Act

            await service.CopyPreviousWeek(weekNumber);

            // Assert

            Assert.Equal(4, entries.Count);
            Assert.Equal(weekNumber + 1, entries[2].Date.WeekNumber());
            Assert.Equal(weekNumber + 1, entries[3].Date.WeekNumber());
            Assert.Equal(entries[2].Date, new DateOnly(2023, 12, 3));
            Assert.Equal(entries[3].Date, new DateOnly(2023, 12, 2));
        }
    }
}

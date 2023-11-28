namespace ClinicApp.Infrastructure
{
    public static class DateOnlyExtensions
    {
        public static int WeekNumber(this DateOnly date) => date.DayNumber / 7;

        public static IEnumerable<DateOnly> GetDatesForWeek(int weekNumber)
        {
            var currentDate = DateOnly.FromDayNumber(weekNumber * 7);
            var endDate = currentDate.AddDays(7);

            while (currentDate < endDate)
            {
                yield return currentDate;
                currentDate = currentDate.AddDays(1);
            }
        }
    }
}

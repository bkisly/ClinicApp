namespace ClinicApp.Infrastructure
{
    public static class DateOnlyExtensions
    {
        public static int WeekNumber(this DateOnly date) => date.DayNumber / 7;
    }
}

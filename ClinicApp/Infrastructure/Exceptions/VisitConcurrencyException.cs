namespace ClinicApp.Infrastructure.Exceptions
{
    public class VisitConcurrencyException(string message, byte[]? dbRowVersion = null) : Exception(message)
    {
        public byte[]? DbRowVersion { get; set; } = dbRowVersion;
    }
}

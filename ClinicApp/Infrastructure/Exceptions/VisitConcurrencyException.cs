using ClinicApp.Models;

namespace ClinicApp.Infrastructure.Exceptions
{
    public class VisitConcurrencyException(string message, Visit? dbRowVersion = null) : Exception(message)
    {
        public Visit? DbEntity { get; set; } = dbRowVersion;
    }
}

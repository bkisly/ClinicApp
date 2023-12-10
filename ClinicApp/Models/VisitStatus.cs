namespace ClinicApp.Models
{
    public class VisitStatus
    {
        public byte Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public enum VisitStatusEnum : byte
    {
        SignedUp = 1,
        Finished,
        Cancelled
    }
}

namespace ClinicApp.ViewModels
{
    public class VisitViewModel
    {
        public DateTime Date { get; set; }
        public string DoctorId { get; set; } = null!;
        public string? PatientId { get; set; }
        public bool IsActivated { get; set; }
        public IEnumerable<DateTime>? AvailableVisits { get; set; }
    }
}
 
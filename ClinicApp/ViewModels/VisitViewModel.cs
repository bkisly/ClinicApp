namespace ClinicApp.ViewModels
{
    public class VisitViewModel
    {
        public string DoctorId { get; set; } = string.Empty;
        public IEnumerable<DateTime> AvailableVisits { get; set; } = null!;
    }
}

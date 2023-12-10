namespace ClinicApp.Models.Dto
{
    public class VisitDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string DoctorId { get; set; } = string.Empty;
        public byte VisitStatusId { get; set; }
    }
}

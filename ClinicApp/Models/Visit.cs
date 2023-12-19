using System.ComponentModel.DataAnnotations;
using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using ClinicApp.Models.ValidationAttributes;

namespace ClinicApp.Models
{
    public class Visit : ICopyable<Visit>
    {
        public int Id { get; set; }
        [MinutesDividibleBy(Constants.VisitDurationMinutes)] public DateTime Date { get; set; }
        public string? Description { get; set; }

        public Patient Patient { get; set; } = null!;
        public string PatientId { get; set; } = null!;

        public Doctor Doctor { get; set; } = null!;
        public string DoctorId { get; set; } = null!;

        public byte VisitStatusId { get; set; }
        public VisitStatus VisitStatus { get; set; } = null!;

        [Timestamp] public byte[] RowVersion { get; set; } = null!;

        public void CopyTo(Visit obj)
        {
            obj.Date = Date;
            obj.Description = Description;
            obj.Patient = Patient;
            obj.PatientId = PatientId;
            obj.Doctor = Doctor;
            obj.DoctorId = DoctorId;
            obj.VisitStatusId = VisitStatusId;
            obj.VisitStatus = VisitStatus;
        }
    }
}

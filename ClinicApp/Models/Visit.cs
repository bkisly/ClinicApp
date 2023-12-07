using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using ClinicApp.Models.ValidationAttributes;

namespace ClinicApp.Models
{
    public class Visit : ICopyable<Visit>
    {
        public int Id { get; set; }
        [MinutesDividibleBy(Constants.VisitDurationMinutes)]
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public Patient Patient { get; set; } = null!;
        public Doctor Doctor { get; set; } = null!;

        public void CopyTo(Visit obj)
        {
            obj.Date = Date;
            obj.Description = Description;
            obj.Patient = Patient;
            obj.Doctor = Doctor;
        }
    }
}

using ClinicApp.Models.Users;

namespace ClinicApp.Areas.Manage.ViewModels
{
    public class PatientsViewModel
    {
        public IEnumerable<Patient> Patients { get; set; } = null!;
        public string SelectedPatientId { get; set; } = string.Empty;
    }
}

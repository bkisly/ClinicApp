using ClinicApp.Models;

namespace ClinicApp.Areas.Manage.ViewModels
{
    public class DoctorRegistrationViewModel
    {
        public IEnumerable<Speciality>? Specialities { get; set; }

        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public byte SpecialityId { get; set; }
    }
}

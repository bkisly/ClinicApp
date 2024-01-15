using System.ComponentModel.DataAnnotations;
using ClinicApp.Models;

namespace ClinicApp.Areas.Manage.ViewModels
{
    public class DoctorRegistrationViewModel
    {
        public IEnumerable<Speciality>? Specialities { get; set; }

        [Required] public string UserName { get; set; } = string.Empty;
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
        [Required] public string ConfirmPassword { get; set; } = string.Empty;
        public byte SpecialityId { get; set; }
    }
}

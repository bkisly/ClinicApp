using ClinicApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClinicApp.Areas.Manage.ViewModels
{
    public class DoctorRegistrationViewModel
    {
        [BindNever] public IEnumerable<Speciality> Specialities { get; set; } = null!;

        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public byte SpecialityId { get; set; }
    }
}

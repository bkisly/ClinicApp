using ClinicApp.Models;
using ClinicApp.Models.Users;

namespace ClinicApp.ViewModels
{
    public class IndexViewModel
    {
        public string? RoleName { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; } = null!;
        public IEnumerable<Speciality> Specialities { get; set; } = null!;
        public int? SelectedSpeciality { get; set; }
        public bool IsActivated { get; set; }
    }
}

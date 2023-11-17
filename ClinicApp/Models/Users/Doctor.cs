using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Models.Users
{
    public class Doctor : IdentityUser
    {
        public bool IsConfirmed { get; set; }
        public Speciality Speciality { get; set; } = null!;
    }
}

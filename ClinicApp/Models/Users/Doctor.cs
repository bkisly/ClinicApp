using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Models.Users
{
    public class Doctor : IdentityUser
    {
        public Speciality Speciality { get; set; } = null!;
    }
}

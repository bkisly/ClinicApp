using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Models.Users
{
    public class Patient : IdentityUser
    {
        public bool IsActivated { get; set; }
    }
}

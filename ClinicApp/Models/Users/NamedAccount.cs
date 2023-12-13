using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Models.Users
{
    public class NamedAccount : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string? DisplayName => string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName)
            ? UserName
            : $"{FirstName} {LastName}";
    }
}

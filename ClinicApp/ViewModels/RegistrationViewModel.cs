using System.ComponentModel.DataAnnotations;

namespace ClinicApp.ViewModels
{
    public class RegistrationViewModel
    {
        [Required] public string UserName { get; set; } = string.Empty;
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public string Password { get; set; } = string.Empty;
        [Required] public string ConfirmPassword { get; set; } = string.Empty;
    }
}

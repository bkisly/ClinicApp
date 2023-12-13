namespace ClinicApp.Models.Users
{
    public class Patient : NamedAccount
    {
        public bool IsActivated { get; set; }
    }
}

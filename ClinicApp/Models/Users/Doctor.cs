namespace ClinicApp.Models.Users
{
    public class Doctor : NamedAccount
    {
        public Speciality Speciality { get; set; } = null!;
    }
}

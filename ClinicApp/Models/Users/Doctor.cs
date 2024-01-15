namespace ClinicApp.Models.Users
{
    public class Doctor : NamedAccount
    {
        public byte SpecialityId { get; set; }
        public Speciality Speciality { get; set; } = null!;
    }
}

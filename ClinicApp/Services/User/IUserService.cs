namespace ClinicApp.Services.User
{
    public interface IUserService
    {
        Task ActivatePatient(string patientId);
    }
}

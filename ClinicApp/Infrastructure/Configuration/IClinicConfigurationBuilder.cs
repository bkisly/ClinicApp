namespace ClinicApp.Infrastructure.Configuration
{
    public interface IClinicConfigurationBuilder
    {
        IClinicConfigurationBuilder BuildManagerCredentials();
        ClinicConfiguration Build();
    }
}

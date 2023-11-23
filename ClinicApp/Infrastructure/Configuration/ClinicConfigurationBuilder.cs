namespace ClinicApp.Infrastructure.Configuration
{
    public class ClinicConfigurationBuilder : IClinicConfigurationBuilder
    {
        private readonly ClinicConfiguration _clinicConfiguration;
        private readonly IConfiguration _configuration;

        public ClinicConfigurationBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
            _clinicConfiguration = new ClinicConfiguration();
        }

        public IClinicConfigurationBuilder BuildManagerCredentials()
        {
            _clinicConfiguration.ManagerUserName = _configuration.GetValue<string>("ManagerUserName") ??
                              throw new NullReferenceException("ManagerUserName not set.");

            _clinicConfiguration.ManagerPassword = _configuration.GetValue<string>("ManagerPassword") ??
                              throw new NullReferenceException("ManagerPassword not set.");

            return this;
        }

        public ClinicConfiguration Build() => _clinicConfiguration;
    }
}

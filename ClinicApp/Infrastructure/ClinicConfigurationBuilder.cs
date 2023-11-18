namespace ClinicApp.Infrastructure
{
    public class ClinicConfigurationBuilder
    {
        private readonly IConfiguration _configuration;

        public string ManagerUserName { get; private set; } = string.Empty;
        public string ManagerPassword { get; private set; } = string.Empty;

        public ClinicConfigurationBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void BuildManagerCredentials()
        {
            ManagerUserName = _configuration.GetValue<string>("ManagerUserName") ??
                              throw new NullReferenceException("ManagerUserName not set.");

            ManagerPassword = _configuration.GetValue<string>("ManagerPassword") ?? 
                              throw new NullReferenceException("ManagerPassword not set.");
        }
    }
}

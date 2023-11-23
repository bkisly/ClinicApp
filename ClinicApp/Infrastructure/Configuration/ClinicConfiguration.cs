namespace ClinicApp.Infrastructure.Configuration;

public record ClinicConfiguration
{
    public string ManagerUserName { get; set; } = string.Empty;
    public string ManagerPassword { get; set; } = string.Empty;
}
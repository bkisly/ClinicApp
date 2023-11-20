namespace ClinicApp.Services.User
{
    public interface IIdentityUserService : IUserService
    {
        Task SignOut();
    }
}

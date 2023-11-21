using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public class IdentityUserService : IIdentityUserService
    {
        public Task RegisterDoctor(string userName, string password, byte specialityId)
        {
            throw new NotImplementedException();
        }

        public Task RegisterPatient(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task RegisterManager(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task SignIn(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task SignOut<TUser>(TUser user) where TUser : IdentityUser
        {
            throw new NotImplementedException();
        }
    }
}

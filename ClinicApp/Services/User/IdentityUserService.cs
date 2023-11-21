using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using ClinicApp.Repositories;
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

        public Task SignOut(IdentityUser user)
        {
            throw new NotImplementedException();
        }
    }
}

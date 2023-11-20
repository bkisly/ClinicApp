using ClinicApp.Infrastructure;
using ClinicApp.Models;
using ClinicApp.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UserManager<Doctor> _doctorUserManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IdentityUserService(UserManager<IdentityUser> userManager, UserManager<Doctor> doctorUserManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _doctorUserManager = doctorUserManager;
            _signInManager = signInManager;
        }

        public async Task RegisterDoctor(string userName, string password, byte specialityId)
        {
            var speciality = new Speciality();
            var doctor = new Doctor { UserName = userName, Speciality = speciality };
        }

        public async Task RegisterPatient(string userName, string password)
        {

        }

        public Task SignIn(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task SignOut()
        {
            throw new NotImplementedException();
        }

        private Task RegisterDoctor(string userName, string password)
        {

        }
    }
}

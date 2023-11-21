using ClinicApp.Infrastructure;
using ClinicApp.Models.Users;
using ClinicApp.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly IUserManagerProvider _userManagerProvider;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ISpecialityRepository _specialityRepository;

        public IdentityUserService(IUserManagerProvider userManagerProvider, SignInManager<IdentityUser> signInManager, ISpecialityRepository specialityRepository)
        {
            _userManagerProvider = userManagerProvider;
            _signInManager = signInManager;
            _specialityRepository = specialityRepository;
        }

        public async Task RegisterDoctor(string userName, string password, byte specialityId)
        {
            var speciality = _specialityRepository.Specialities.Single(s => s.Id == specialityId);
            var doctor = new Doctor { UserName = userName, Speciality = speciality };
            var mgr = _userManagerProvider.Provide(doctor);

            await mgr.CreateAsync(doctor, password);
            await mgr.AddToRoleAsync(doctor, Constants.Roles.DoctorRoleName);
        }

        public async Task RegisterPatient(string userName, string password)
        {
            var patient = new Patient { UserName = userName };
            var mgr = _userManagerProvider.Provide(patient);

            await mgr.CreateAsync(patient);
            await mgr.AddToRoleAsync(patient, Constants.Roles.PatientRoleName);
        }

        public async Task RegisterManager(string userName, string password)
        {
            var manager = new Manager { UserName = userName };
            var mgr = _userManagerProvider.Provide(manager);

            await mgr.CreateAsync(manager);
            await mgr.AddToRoleAsync(manager, Constants.Roles.ManagerRoleName);
        }

        public async Task SignIn(string userName, string password)
        {
            var mgr = _userManagerProvider.DefaultManager;
            var user = await mgr.FindByNameAsync(userName);
            var valid = user != null && await mgr.CheckPasswordAsync(user, password);

            if (!valid)
                throw new ArgumentException("Invalid credentials.");

            await _signInManager.SignInAsync(user!, true);
        }

        public async Task SignOut(IdentityUser user)
        {
            await _signInManager.SignOutAsync();
        }
    }
}

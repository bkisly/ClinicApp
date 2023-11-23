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

        public async Task<RegistrationResult> RegisterDoctor(string userName, string password, byte specialityId)
        {
            var speciality = _specialityRepository.Specialities.Single(s => s.Id == specialityId);
            var doctor = new Doctor { UserName = userName, Speciality = speciality };
            var mgr = _userManagerProvider.Provide(doctor);

            if (await mgr.FindByNameAsync(userName) != null)
                return RegistrationResult.UserExists;

            await mgr.CreateAsync(doctor, password);
            await mgr.AddToRoleAsync(doctor, Constants.Roles.DoctorRoleName);

            return RegistrationResult.Succeeded;
        }

        public async Task<RegistrationResult> RegisterPatient(string userName, string password)
        {
            var patient = new Patient { UserName = userName };
            var mgr = _userManagerProvider.Provide(patient);

            if (await mgr.FindByNameAsync(userName) != null)
                return RegistrationResult.UserExists;

            await mgr.CreateAsync(patient, password);
            await mgr.AddToRoleAsync(patient, Constants.Roles.PatientRoleName);

            return RegistrationResult.Succeeded;
        }

        public async Task<RegistrationResult> RegisterManager(string userName, string password)
        {
            var manager = new Manager { UserName = userName };
            var mgr = _userManagerProvider.Provide(manager);

            if (await mgr.FindByNameAsync(userName) != null)
                return RegistrationResult.UserExists;

            await mgr.CreateAsync(manager, password);
            await mgr.AddToRoleAsync(manager, Constants.Roles.ManagerRoleName);

            return RegistrationResult.Succeeded;
        }

        public async Task<SignInResult> SignIn(string userName, string password)
        {
            var mgr = _userManagerProvider.DefaultManager;
            var user = await mgr.FindByNameAsync(userName);
            var valid = user != null && await mgr.CheckPasswordAsync(user, password);

            if (!valid)
                throw new ArgumentException("Invalid credentials.");

            return await _signInManager.PasswordSignInAsync(userName, password, false, false);
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}

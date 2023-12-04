using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public class RegistrationService(IUserDependenciesProvider userDependenciesProvider) : IRegistrationService
    {
        /*public async Task<RegistrationResult> RegisterDoctor(string userName, string password, byte specialityId)
        {
            var speciality = specialityRepository.Specialities.Single(s => s.Id == specialityId);
            var doctor = new Doctor { UserName = userName, Speciality = speciality };
            var mgr = userManagerProvider.Provide(doctor);

            if (await mgr.FindByNameAsync(userName) != null)
                return RegistrationResult.UserExists;

            try
            {
                await mgr.CreateAsync(doctor, password);
                await mgr.AddToRoleAsync(doctor, Constants.Roles.DoctorRoleName);
            }
            catch (Exception)
            {
                throw new ArgumentException(
                    "Password is not valid. Requirements: min. 8 characters long, containing at least 1 small, 1 capital letter, 1 number and 1 symbol.");
            }

            return RegistrationResult.Succeeded;
        }

        public async Task<RegistrationResult> RegisterPatient(string userName, string password)
        {
            var patient = new Patient { UserName = userName };
            var mgr = userManagerProvider.Provide(patient);

            if (await mgr.FindByNameAsync(userName) != null)
                return RegistrationResult.UserExists;

            try
            {
                await mgr.CreateAsync(patient, password);
                await mgr.AddToRoleAsync(patient, Constants.Roles.PatientRoleName);
            }
            catch (Exception)
            {
                throw new ArgumentException(
                    "Password is not valid. Requirements: min. 8 characters long, containing at least 1 small, 1 capital letter, 1 number and 1 symbol.");
            }

            return RegistrationResult.Succeeded;
        }

        public async Task<RegistrationResult> RegisterManager(string userName, string password)
        {
            var manager = new Manager { UserName = userName };
            var mgr = userManagerProvider.Provide(manager);

            if (await mgr.FindByNameAsync(userName) != null)
                return RegistrationResult.UserExists;

            try
            {
                await mgr.CreateAsync(manager, password);
                await mgr.AddToRoleAsync(manager, Constants.Roles.ManagerRoleName);
            }
            catch (Exception)
            {
                throw new ArgumentException(
                    "Password is not valid. Requirements: min. 8 characters long, containing at least 1 small, 1 capital letter, 1 number and 1 symbol.");
            }

            return RegistrationResult.Succeeded;
        }*/

        public async Task<RegistrationResult> RegisterAsync<TUser>(TUser user, string password) where TUser : IdentityUser
        {
            var manager = userDependenciesProvider.ProvideManager(user);
            if (await manager.FindByNameAsync(user.UserName ?? string.Empty) != null)
                return RegistrationResult.UserExists;

            try
            {
                var result = await manager.CreateAsync(user, password);
                var roleName = userDependenciesProvider.ProvideRoleName(user);
                if (roleName != null)
                    await manager.AddToRoleAsync(user, roleName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RegistrationResult.Succeeded;
        }
    }
}

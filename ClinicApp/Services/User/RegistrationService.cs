using Microsoft.AspNetCore.Identity;

namespace ClinicApp.Services.User
{
    public class RegistrationService(IUserDependenciesProvider userDependenciesProvider) : IRegistrationService
    {
        public async Task<IdentityResult> RegisterAsync<TUser>(TUser user, string password) where TUser : IdentityUser
        {
            var manager = userDependenciesProvider.ProvideManager(user);

            if (await userDependenciesProvider.DefaultManager.FindByNameAsync(user.UserName ?? string.Empty) != null)
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserExists", 
                    Description = "User with the specified name already exists."
                });

            var result = await manager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                var roleName = userDependenciesProvider.ProvideRoleName(user);
                if (roleName != null)
                    await manager.AddToRoleAsync(user, roleName);
            }

            return result;
        }
    }
}

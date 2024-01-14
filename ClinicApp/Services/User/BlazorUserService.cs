using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClinicApp.Services.User
{
    public class BlazorUserService(AuthenticationStateProvider authenticationStateProvider) : IBlazorUserService
    {
        public ClaimsPrincipal User => GetCurrentUser().Result;

        private async Task<ClaimsPrincipal> GetCurrentUser()
        {
            var state = await authenticationStateProvider.GetAuthenticationStateAsync();
            return state.User;
        }
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClinicApp.Pages
{
    public class AuthorizableComponentBase : ComponentBase
    {
        [CascadingParameter] protected Task<AuthenticationState>? AuthenticationState { get; set; }
    }
}

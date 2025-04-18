using Microsoft.AspNetCore.Components.Authorization;
using SalesSystemWebApp.ViewModels.Registers;

namespace SalesSystemWebApp.Security
{
    public interface ICustomAuthenticationStateProvider
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();
        Task UpdateAuthenticationState(UserSessionViewModel? userSession);
    }
}

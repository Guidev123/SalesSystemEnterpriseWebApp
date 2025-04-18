using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using SalesSystemWebApp.Extensions;
using SalesSystemWebApp.ViewModels.Registers;
using System.Security.Claims;

namespace SalesSystemWebApp.Security
{
    public class CustomAuthenticationStateProvider(ISessionStorageService sessionStorage, IJSRuntime jSRuntime) : AuthenticationStateProvider, ICustomAuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = await sessionStorage.ReadEncryptedItemAsync<UserSessionViewModel>("UserSession", jSRuntime);
                if (userSession is null || userSession.UserToken is null || string.IsNullOrEmpty(userSession.UserToken.Email))
                {
                    return new AuthenticationState(_anonymous);
                }

                if (userSession.ExpiryTimeStamp <= DateTime.Now)
                {
                    await sessionStorage.RemoveItemAsync("UserSession");
                    return new AuthenticationState(_anonymous);
                }

                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, userSession.UserToken.Email),
                    new(ClaimTypes.Email, userSession.UserToken.Email),
                    new(ClaimTypes.NameIdentifier, userSession.UserToken.Id.ToString())
                };

                claims.AddRange(userSession.UserToken.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

                claims.AddRange(userSession.UserToken.Claims.Select(c => new Claim(c.Type, c.Value)));

                var identity = new ClaimsIdentity(claims, "JwtAuth");
                var principal = new ClaimsPrincipal(identity);

                return new AuthenticationState(principal);
            }
            catch 
            {
                return new AuthenticationState(_anonymous);
            }
        }

        public async Task UpdateAuthenticationState(UserSessionViewModel? userSession)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal;

                if (userSession?.UserToken != null && !string.IsNullOrEmpty(userSession.UserToken.Email))
                {
                    var claims = new List<Claim>
                    {
                        new(ClaimTypes.Name, userSession.UserToken.Email),
                        new(ClaimTypes.Email, userSession.UserToken.Email),
                        new(ClaimTypes.NameIdentifier, userSession.UserToken.Id.ToString())
                    };

                    claims.AddRange(userSession.UserToken.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
                    claims.AddRange(userSession.UserToken.Claims.Select(c => new Claim(c.Type, c.Value)));

                    var identity = new ClaimsIdentity(claims, "JwtAuth");
                    claimsPrincipal = new ClaimsPrincipal(identity);

                    userSession.ExpiryTimeStamp = DateTime.Now.AddSeconds(userSession.ExpiresIn);
                    await sessionStorage.SaveItemEncryptedAsync("UserSession", userSession, jSRuntime);
                }
                else
                {
                    claimsPrincipal = _anonymous;
                    await sessionStorage.RemoveItemAsync("UserSession");
                }

                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            }
            catch (Exception)
            {
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
            }
        }

        public async Task<string> GetToken()
        {
            try
            {
                var userSession = await sessionStorage.ReadEncryptedItemAsync<UserSessionViewModel>("UserSession", jSRuntime);
                if (userSession is not null && userSession.ExpiryTimeStamp > DateTime.Now)
                {
                    return userSession.AccessToken;
                }
            }
            catch
            {   }

            return string.Empty;
        }
    }
}

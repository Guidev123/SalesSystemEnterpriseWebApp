using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SalesSystemWebApp.Extensions;
using SalesSystemWebApp.ViewModels.Registers;
using System.Security.Claims;

namespace SalesSystemWebApp.Security
{
    public class CustomAuthenticationStateProvider(ISessionStorageService sessionStorage) : AuthenticationStateProvider, ICustomAuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage = sessionStorage ?? throw new ArgumentNullException(nameof(sessionStorage));
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = await _sessionStorage.ReadEncryptedItemAsync<UserSessionViewModel>("UserSession");
                if (userSession is null || userSession.UserToken is null || string.IsNullOrEmpty(userSession.UserToken.Email))
                {
                    return new AuthenticationState(_anonymous);
                }

                if (userSession.ExpiryTimeStamp <= DateTime.Now)
                {
                    await _sessionStorage.RemoveItemAsync("UserSession");
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
            catch (Exception ex)
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
                    await _sessionStorage.SaveItemEncryptedAsync("UserSession", userSession);
                }
                else
                {
                    claimsPrincipal = _anonymous;
                    await _sessionStorage.RemoveItemAsync("UserSession");
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
                var userSession = await _sessionStorage.ReadEncryptedItemAsync<UserSessionViewModel>("UserSession");
                if (userSession != null && userSession.ExpiryTimeStamp > DateTime.Now)
                {
                    return userSession.AccessToken;
                }
            }
            catch
            {
            }

            return string.Empty;
        }
    }
}

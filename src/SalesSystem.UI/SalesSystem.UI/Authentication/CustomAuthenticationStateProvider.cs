using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SalesSystem.UI.Authentication
{
    public class CustomAuthenticationStateProvider(ProtectedLocalStorage protectedLocalStorage) : AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = (await protectedLocalStorage.GetAsync<string>("accessToken").ConfigureAwait(false)).Value;
            var identity = string.IsNullOrEmpty(token) ? new ClaimsIdentity() : GetClaims(token);
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public async Task MarkUserAsAuthenticatedAsync(string token)
        {
            await protectedLocalStorage.SetAsync("accessToken", token).ConfigureAwait(false);
            var identity = GetClaims(token);
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private static ClaimsIdentity GetClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            return new ClaimsIdentity(claims, "jwt");
        }

        public async Task MarkUserAsLogout()
        {
            await protectedLocalStorage.DeleteAsync("accessToken").ConfigureAwait(false);
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }
    }
}

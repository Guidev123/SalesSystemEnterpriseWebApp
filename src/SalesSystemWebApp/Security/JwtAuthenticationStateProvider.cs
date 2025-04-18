using Microsoft.AspNetCore.Components.Authorization;
using SalesSystemWebApp.Security.Token;
using SalesSystemWebApp.Services.Register;
using SalesSystemWebApp.ViewModels;
using SalesSystemWebApp.ViewModels.Registers;
using System.Security.Claims;

namespace SalesSystemWebApp.Security
{
    public class JwtAuthenticationStateProvider(IRegistersService registerService, ITokenService tokenService)
        : AuthenticationStateProvider, IJwtAuthenticationStateProvider
    {
        private readonly ITokenService _tokenService = tokenService;
        private readonly IRegistersService _registerService = registerService;
        private bool _isAuthenticated = false;

        public async Task<bool> CheckAuthenticatedAsync()
        {
            await GetAuthenticationStateAsync();
            return _isAuthenticated;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _isAuthenticated = false;

            var user = new ClaimsPrincipal(new ClaimsIdentity());
            var userInfo = await GetUser();

            if (userInfo is null || userInfo.Data is null) return new AuthenticationState(user);

            var claims = GetClaims(userInfo.Data);

            var id = new ClaimsIdentity(claims, nameof(JwtAuthenticationStateProvider));
            user = new ClaimsPrincipal(id);

            _isAuthenticated = true;
            return new AuthenticationState(user);
        }

        public void NotifyAuthenticationStateChanged()
            => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        private async Task<ResponseViewModel<UserViewModel?>?> GetUser()
        {
            try
            {
                return await _registerService.GetAsync();
            }
            catch
            {
                return null;
            }
        }

        private static List<Claim> GetClaims(UserViewModel user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Email),
                new(ClaimTypes.Email, user.Email)
            };

            claims.AddRange
            (
               user.Roles.Select(x => new Claim("roles", x))
            );

            return claims;
        }

        public async Task SetTokenAsync(string token)
        {
            await _tokenService.SetToken(token);
            NotifyAuthenticationStateChanged();
        }

        public async Task RemoveTokenAsync()
        {
            await _tokenService.RemoveToken();
            NotifyAuthenticationStateChanged();
        }
    }
}

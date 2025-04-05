﻿using Microsoft.AspNetCore.Components.Authorization;
using SalesSystemWebApp.Responses;
using SalesSystemWebApp.Security.Token;
using SalesSystemWebApp.Services.Register;
using SalesSystemWebApp.ViewModels;
using System.Security.Claims;

namespace SalesSystemWebApp.Security
{
    public class JwtAuthenticationStateProvider(IRegisterService registerService, ITokenService tokenService)
        : AuthenticationStateProvider, IJwtAuthenticationStateProvider
    {
        private readonly ITokenService _tokenService = tokenService;
        private readonly IRegisterService _registerService = registerService;
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

        private async Task<Response<UserViewModel>?> GetUser()
        {
            try
            {
                var response = await _registerService.GetAsync();
                return response;
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
               user.Claims.Where(x => x.Key != ClaimTypes.Name && x.Value != ClaimTypes.Email).Select(x => new Claim(x.Key, x.Value))
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

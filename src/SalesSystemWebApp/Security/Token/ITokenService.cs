﻿namespace SalesSystemWebApp.Security.Token
{
    public interface ITokenService
    {
        Task<string?> GetTokenAsync();
        Task SetToken(string token);
        Task RemoveToken();
    }
}

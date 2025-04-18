using Blazored.SessionStorage;
using Microsoft.JSInterop;
using SalesSystemWebApp.Extensions;
using SalesSystemWebApp.ViewModels.Registers;
using System.Net.Http.Headers;

namespace SalesSystemWebApp.Security
{
    public class JwtHandler(ISessionStorageService sessionStorageService, IJSRuntime jSRuntime) : DelegatingHandler
    {

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await sessionStorageService.ReadEncryptedItemAsync<UserSessionViewModel>("UserSession", jSRuntime);
            if (token is not null && !string.IsNullOrEmpty(token.AccessToken))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
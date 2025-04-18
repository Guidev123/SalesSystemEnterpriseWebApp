using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesSystemWebApp.Security.Token;
using System.Net;

namespace SalesSystemWebApp.Security
{
    public class UnauthorizedResponseHandler(NavigationManager navigation,
                                             ISnackbar snackbar,
                                             ITokenService tokenService) : DelegatingHandler
    {

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await tokenService.RemoveToken();
                navigation.NavigateTo("/sign-in", forceLoad: true);
                snackbar.Add("Session expired, please login again.", Severity.Warning);
            }

            return response;
        }
    }

}

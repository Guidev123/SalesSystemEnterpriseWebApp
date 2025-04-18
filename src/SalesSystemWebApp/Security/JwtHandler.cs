using SalesSystemWebApp.Security.Token;
using System.Net.Http.Headers;

namespace SalesSystemWebApp.Security
{
    public class JwtHandler(ITokenService tokenService) : DelegatingHandler
    {

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await tokenService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
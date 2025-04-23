using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SalesSystem.UI.Services
{
    public abstract class Service(ProtectedLocalStorage protectedLocalStorage)
    {
        protected async Task<T?> DeserializeObjectResponse<T>(HttpResponseMessage response)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            };

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(json, options);
        }

        protected StringContent GetContent(object data)
            => new(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

        protected async Task SetTokenAsync(HttpClient httpClient)
        {
            var token = (await protectedLocalStorage.GetAsync<string>("accessToken").ConfigureAwait(false)).Value;
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}

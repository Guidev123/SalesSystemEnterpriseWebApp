using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SalesSystem.UI.Authentication;
using SalesSystem.UI.Extensions;
using SalesSystem.UI.Services;
using SalesSystem.UI.Services.Interfaces;
using System.Net.Http.Headers;

namespace SalesSystem.UI.Configurations
{
    public static class WebConfiguration
    {
        public static void AddWebConfiguration(this WebApplicationBuilder builder)
        {
            builder.RegisterModelsSettings();
            builder.AddServices();
            builder.AddHttpClientServices();
        }

        private static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        }

        private static void AddHttpClientServices(this WebApplicationBuilder builder)
        {
            builder.AddHttpClientService<IRegistersService, RegistersService>(settings => settings.ServiceUrl);
        }

        private static void AddHttpClientService<TInterface, TImplementation>(
            this WebApplicationBuilder builder,
            Func<AppServicesSettings, string> getServiceUrl)
            where TInterface : class
            where TImplementation : class, TInterface
        {
            builder.Services.AddHttpClient<TInterface, TImplementation>((serviceProvider, httpClient) =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<AppServicesSettings>>().Value;

                httpClient.BaseAddress = new Uri(getServiceUrl(settings));
            })
            .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(5)
            })
            .SetHandlerLifetime(Timeout.InfiniteTimeSpan);
        }

        private static void RegisterModelsSettings(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<AppServicesSettings>(
                builder.Configuration.GetSection(nameof(AppServicesSettings)));
        }
    }
}

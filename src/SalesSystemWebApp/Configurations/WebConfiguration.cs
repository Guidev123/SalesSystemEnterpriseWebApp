using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using SalesSystemWebApp.Security;
using SalesSystemWebApp.Services.Catalog;
using SalesSystemWebApp.Services.Register;
using SalesSystemWebApp.Services.Sales;

namespace SalesSystemWebApp.Configurations
{
    public static class WebConfiguration
    {
        public static string BackendUrl { get; set; } = string.Empty;

        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRestServices();

            services.AddDelegatingHandlers();

            services.AddHttpClientConfig();

            services.AddAuthConfig();

            services.AddMudServices();
        }

        public static void AddRestServices(this IServiceCollection services)
        {
            services.AddTransient<IRegistersService, RegistersService>();
            services.AddTransient<ISalesService, SalesService>();
            services.AddTransient<ICatalogService, CatalogService>();
            services.AddSingleton<ICartService, CartService>();
        }

        public static void AddDelegatingHandlers(this IServiceCollection services)
        {
            services.AddTransient<JwtHandler>();
            services.AddTransient<UnauthorizedResponseHandler>();
        }

        public static void AddAuthConfig(this IServiceCollection services)
        {
            services.AddAuthorizationCore();
            services.AddBlazoredSessionStorage();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddScoped(x => (ICustomAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());
        }

        public static void AddHttpClientConfig(this IServiceCollection services)
        {
            services.AddHttpClient("SalesSystem", options =>
            {
                options.BaseAddress = new Uri(BackendUrl);
            }).AddHttpMessageHandler<JwtHandler>()
              .AddHttpMessageHandler<UnauthorizedResponseHandler>();
        }
    }
}

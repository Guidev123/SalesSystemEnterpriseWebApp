using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using SalesSystemWebApp.Security;
using SalesSystemWebApp.Security.Token;
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
            services.AddTransient<IRegistersService, RegistersService>();
            services.AddTransient<ISalesService, SalesService>();
            services.AddTransient<ICatalogService, CatalogService>();
            services.AddSingleton<ICartService, CartService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<JwtHandler>();
            services.AddScoped<UnauthorizedResponseHandler>();
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
            services.AddScoped(x => (IJwtAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());

            services.AddMudServices();

            services.AddHttpClient("SalesSystem", options =>
            {
                options.BaseAddress = new Uri(BackendUrl);
            }).AddHttpMessageHandler<JwtHandler>()
            .AddHttpMessageHandler<UnauthorizedResponseHandler>();
        }
    }
}

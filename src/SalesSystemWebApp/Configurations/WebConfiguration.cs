using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using SalesSystemWebApp.Security.Token;
using SalesSystemWebApp.Security;
using SalesSystemWebApp.Services.Register;

namespace SalesSystemWebApp.Configurations
{
    public static class WebConfiguration
    {
        public const string HTTP_CLIENT_NAME = "SalesSystem";
        public static string BackendUrl { get; set; } = string.Empty;

        public static void AddDependencies(this IServiceCollection services)
        {
            // SERVICES
            services.AddTransient<IRegisterService, RegisterService>();

            // SECURITY
            services.AddTransient<ITokenService, TokenService>();
            services.AddScoped<JwtHandler>();
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
            services.AddScoped(x => (IJwtAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());

            // TEMPLATE
            services.AddMudServices();

            // HTTP CLIENT
            services.AddHttpClient(HTTP_CLIENT_NAME, options =>
            {
                options.BaseAddress = new Uri(BackendUrl);
            }).AddHttpMessageHandler<JwtHandler>();
        }
    }
}

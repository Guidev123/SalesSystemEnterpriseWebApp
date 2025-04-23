using MudBlazor.Services;
using SalesSystem.UI.Components;
using SalesSystem.UI.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();

builder.Services.AddAuthentication();
builder.Services.AddCascadingAuthenticationState();

builder.AddWebConfiguration();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SalesSystem.UI.Client._Imports).Assembly);

app.UseAuthentication();
app.UseAuthorization(); 

app.Run();

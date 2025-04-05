using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesSystemWebApp.Security;
using SalesSystemWebApp.Services.Register;
using SalesSystemWebApp.ViewModels;

namespace SalesSystemWebApp.Pages.Register
{
    public partial class SignInPage : ComponentBase
    {
        [Inject]
        public IJwtAuthenticationStateProvider JwtAuthenticationStateProvider { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public IRegisterService UserService { get; set; } = default!;

        public LoginViewModel InputModel { get; set; } = new();

        public bool IsBusy { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var authState = await JwtAuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
                NavigationManager.NavigateTo("/");
        }

        protected async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await UserService.LoginAsync(InputModel);
                if (result.IsSuccess)
                {
                    await JwtAuthenticationStateProvider.SetTokenAsync(result.Data!.AccessToken);
                    NavigationManager.NavigateTo("/");
                    Snackbar.Add("Welcome!", Severity.Success);
                }
                else
                {
                    foreach (var item in result.Errors!)
                        Snackbar.Add(item, Severity.Error);
                }
            }
            catch
            {
                Snackbar.Add("Something has failed", Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

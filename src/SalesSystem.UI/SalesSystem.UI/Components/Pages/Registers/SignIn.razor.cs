using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using SalesSystem.UI.Authentication;
using SalesSystem.UI.Services.Interfaces;
using SalesSystem.UI.ViewModels;

namespace SalesSystem.UI.Components.Pages.Registers
{
    public partial class SignInPage : ComponentBase
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = default!; 

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public IRegistersService RegistersService { get; set; } = default!;

        public SignInViewModel InputModel { get; set; } = new();

        public bool IsBusy { get; set; } = false;

        protected async Task OnValidSubmitAsync()
        {
            var res = await RegistersService.SignInAsync(InputModel).ConfigureAwait(false);
            if(res is null)
            {
                NavigationManager.NavigateTo("/error");
                Snackbar.Add("Something has failed during your Sign In, try again later.", Severity.Error);
                return;
            }

            if (!res.IsSuccess || res.Data is null)
            {
                Snackbar.Add("Something has failed during your Sign In, try again later.", Severity.Error);
                return;
            }

            await ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsAuthenticatedAsync(res.Data.AccessToken);
            NavigationManager.NavigateTo("/");
        }
    }
}

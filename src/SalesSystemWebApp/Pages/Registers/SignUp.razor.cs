﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesSystemWebApp.Security;
using SalesSystemWebApp.Services.Register;
using SalesSystemWebApp.ViewModels.Registers;

namespace SalesSystemWebApp.Pages.Registers
{
    public partial class SignUpPage : ComponentBase
    {

        [Inject]
        public ICustomAuthenticationStateProvider CustomAuthenticationStateProvider { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public IRegistersService UserService { get; set; } = default!;

        public SignUpViewModel InputModel { get; set; } = new();

        public bool IsBusy { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var authState = await CustomAuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity is not null && user.Identity.IsAuthenticated)
                NavigationManager.NavigateTo("/");
        }

        protected async Task OnValidSubmitAsync()
        {
            IsBusy = true;

            try
            {
                var result = await UserService.SignUpAsync(InputModel);
                if (result is null)
                {
                    Snackbar.Add("Something has failed", Severity.Error);
                    return;
                }

                if (result.IsSuccess)
                {
                    NavigationManager.NavigateTo("/sign-in");
                    Snackbar.Add("Account created!", Severity.Success);
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

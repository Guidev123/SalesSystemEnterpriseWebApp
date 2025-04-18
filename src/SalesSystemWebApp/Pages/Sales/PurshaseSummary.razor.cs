using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesSystemWebApp.Services.Sales;
using SalesSystemWebApp.ViewModels.Sales;

namespace SalesSystemWebApp.Pages.Sales
{
    public partial class ShoppingCartPage : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public ISalesService SalesService { get; set; } = default!;

        public CartViewModel? Cart;
        public bool IsLoading = true;
        public string? ErrorMessage;

        protected override async Task OnInitializedAsync()
        {
            await LoadCartAsync();
        }

        private async Task LoadCartAsync()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = null;

                var response = await SalesService.GetCartAsync();

                if (response?.IsSuccess is true && response.Data is not null)
                {
                    Cart = response.Data;
                }
                else
                {
                    ErrorMessage = response?.Message ?? "Error loading cart, contact our support.";
                    Snackbar.Add(ErrorMessage, Severity.Error);
                }
            }
            catch
            {
                ErrorMessage = "Error loading cart, contact our support.";
                Snackbar.Add(ErrorMessage, Severity.Error);
            }
            finally
            {
                IsLoading = false;
                StateHasChanged();
            }
        }
    }
}

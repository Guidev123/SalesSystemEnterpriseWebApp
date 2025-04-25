using Microsoft.AspNetCore.Components;
using SalesSystem.UI.Services.Interfaces;
using SalesSystem.UI.ViewModels;

namespace SalesSystem.UI.Components.Pages.Sales
{
    public partial class CartPage : ComponentBase
    {
        [Inject]
        private ISalesService SalesService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        public ResponseViewModel<OrderSummaryViewModel?>? Response;
        public bool IsLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadCartSummaryAsync();
        }

        public async Task LoadCartSummaryAsync()
        {
            try
            {
                IsLoading = true;
                Response = await SalesService.GetOrderSummaryAsync();
            }
            catch (Exception)
            {
                NavigationManager.NavigateTo("/error");
            }
            finally
            {
                IsLoading = false;
                StateHasChanged();
            }
        }

        public void ContinueShopping()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}

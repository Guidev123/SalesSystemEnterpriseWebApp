using Microsoft.AspNetCore.Components;
using SalesSystemWebApp.Security;
using SalesSystemWebApp.Services.Sales;

namespace SalesSystemWebApp.Components
{
    public partial class CartIcon : ComponentBase
    {
        [Inject]
        public IJwtAuthenticationStateProvider JwtAuthenticationStateProvider { get; set; } = default!;

        [Inject]
        public ICartService CartStateService { get; set; } = default!;

        [Inject]
        public ISalesService SalesService { get; set; } = default!;

        [Parameter]
        public string? Class { get; set; }

        public int CartItemCount { get; set; }
        public bool IsAuthenticated { get; set; }

        protected override async Task OnInitializedAsync()
        {
            IsAuthenticated = await JwtAuthenticationStateProvider.CheckAuthenticatedAsync();
            await LoadCartItemCountAsync();
            CartStateService.OnCartChanged += async () => await LoadCartItemCountAsync();
        }

        public async Task LoadCartItemCountAsync()
        {
            if (!IsAuthenticated)
            {
                CartItemCount = 0;
                await InvokeAsync(StateHasChanged);
                return;
            }

            try
            {
                var response = await SalesService.GetCartAsync();
                if (response?.StatusCode == 401)
                {
                    CartItemCount = 0;
                    return;
                }

                if (response?.IsSuccess == true && response.Data != null)
                {
                    CartItemCount = response.Data.Items?.Sum(item => item.Quantity) ?? 0;
                }
                else
                {
                    CartItemCount = 0;
                }
            }
            catch
            {
                CartItemCount = 0;
            }
            finally
            {
                await InvokeAsync(StateHasChanged);
            }
        }

        public void Dispose()
        {
            CartStateService.OnCartChanged -= async () => await LoadCartItemCountAsync();
        }
    }
}
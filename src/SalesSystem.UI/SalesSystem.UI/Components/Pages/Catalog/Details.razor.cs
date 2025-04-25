using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using SalesSystem.UI.Authentication;
using SalesSystem.UI.Services.Interfaces;
using SalesSystem.UI.ViewModels.Catalog;
using SalesSystem.UI.ViewModels.Responses;
using SalesSystem.UI.ViewModels.Sales;

namespace SalesSystem.UI.Components.Pages.Catalog
{
    public partial class DetailsPage : ComponentBase
    {
        [Parameter]
        public Guid ProductId { get; set; }

        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        private ICatalogService CatalogService { get; set; } = default!;

        [Inject]
        private ISalesService SalesService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        public ResponseViewModel<ProductDetailsViewModel?>? Response;
        public bool IsLoading = true;
        public int Quantity = 1;

        protected override async Task OnInitializedAsync()
        {
            await LoadProductDetailsAsync();
        }

        private async Task LoadProductDetailsAsync()
        {
            try
            {
                IsLoading = true;
                Response = await CatalogService.GetProductByIdAsync(ProductId);
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

        public async Task AddToCart()
        {
            var authState = await ((CustomAuthenticationStateProvider)AuthenticationStateProvider).GetAuthenticationStateAsync();
            if (!authState.User.Identity?.IsAuthenticated ?? false)
            {
                NavigationManager.NavigateTo("sign-in");
                Snackbar.Add("You need be authenticated to add items to your order", Severity.Success);
                return;
            }

            if (Response?.Data == null) return;

            var product = Response.Data.Product;
            var orderItem = new AddOrderItemViewModel(
                product.Id,
                product.Name,
                Quantity,
                product.Price
            );

            try
            {
                var result = await SalesService.AddOrderItemAsync(orderItem);
                if (result?.IsSuccess == true)
                {
                    NavigationManager.NavigateTo("/cart", true);
                    Snackbar.Add("Item added to order!", Severity.Success);
                }
                else
                {
                    NavigationManager.NavigateTo("/");
                    Snackbar.Add("Failed to add item to cart", Severity.Error);
                }
            }
            catch (Exception)
            {
                NavigationManager.NavigateTo("/error");
                Snackbar.Add("Failed to add item to cart", Severity.Error);
            }
            finally
            {
                StateHasChanged();
            }
        }

        public void GoBack()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}

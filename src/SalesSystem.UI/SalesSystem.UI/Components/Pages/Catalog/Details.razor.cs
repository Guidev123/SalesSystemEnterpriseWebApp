using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SalesSystem.UI.Services.Interfaces;
using SalesSystem.UI.ViewModels;

namespace SalesSystem.UI.Components.Pages.Catalog
{
    public partial class DetailsPage : ComponentBase
    {
        [Parameter]
        public Guid ProductId { get; set; }

        [Inject]
        private ICatalogService CatalogService { get; set; } = default!;

        [Inject]
        private ISalesService SalesService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; } = default!;

        public ResponseViewModel<ProductDetailsViewModel?>? Response;
        public bool IsLoading = true;

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
            var authState = await AuthenticationStateTask;
            if (!authState.User.Identity?.IsAuthenticated ?? false)
            {
                NavigationManager.NavigateTo("sign-in");
                return;
            }

            if (Response?.Data == null) return;

            var product = Response.Data.Product;
            var orderItem = new AddOrderItemViewModel(
                product.Id,
                product.Name,
                1,
                product.Price
            );

            try
            {
                var result = await SalesService.AddOrderItemAsync(orderItem);
                if (result?.IsSuccess == true)
                {
                    NavigationManager.NavigateTo("/cart");
                }
                else
                {
                    Response = new ResponseViewModel<ProductDetailsViewModel?>(Response.Data, false, new List<string> { "Failed to add item to cart" }, null);
                }
            }
            catch (Exception)
            {
                Response = new ResponseViewModel<ProductDetailsViewModel?>(Response.Data, false, new List<string> { "An error occurred while adding to cart" }, null);
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

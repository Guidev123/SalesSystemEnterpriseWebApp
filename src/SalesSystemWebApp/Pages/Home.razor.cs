using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesSystemWebApp.Components;
using SalesSystemWebApp.Services.Catalog;
using SalesSystemWebApp.Services.Sales;
using SalesSystemWebApp.ViewModels;

namespace SalesSystemWebApp.Pages
{
    public partial class HomePage : ComponentBase
    {
        private const int DEFAULT_PAGE_SIZE = 8;
        private const int DEFAULT_PAGE_NUMBER = 1;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public ISalesService SalesService { get; set; } = default!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public ICartService CartService { get; set; } = default!;

        [Inject]
        public ICatalogService CatalogService { get; set; } = default!;

        public PagedRequestViewModel InputModel { get; set; } = new PagedRequestViewModel(DEFAULT_PAGE_NUMBER, DEFAULT_PAGE_SIZE);
        public PagedResponseViewModel<CatalogViewModel?>? response;
        public bool IsLoading = true;
        public string? ErrorMessage;

        protected override async Task OnInitializedAsync() 
            => await LoadProductsAsync();

        private async Task LoadProductsAsync()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = null;

                response = await CatalogService.GetAllAsync(InputModel);

                if (response is null || !response.IsSuccess)
                {
                    ErrorMessage = "Error loading products, contact our support.";
                    Snackbar.Add(ErrorMessage, Severity.Error);
                }
            }
            catch
            {
                ErrorMessage = $"Error loading products, contact our support.";
                Snackbar.Add(ErrorMessage, Severity.Error);
            }
            finally
            {
                IsLoading = false;
                StateHasChanged();
            }
        }

        public async Task HandlePageChanged(int page)
        {
            if (IsLoading) return; 
            InputModel = InputModel with { PageNumber = page };
            await LoadProductsAsync();
        }

        public async Task HandlePageSizeChanged(int pageSize)
        {
            InputModel = InputModel with { PageSize = pageSize, PageNumber = 1 }; 
            await LoadProductsAsync();
        }

        public async Task AddToCart(ProductsViewModel product)
        {
            try
            {
                var orderItem = new OrderItemViewModel(
                    product.Id,
                    product.Name,
                    1,
                    product.Price
                );

                var response = await SalesService.AddOrderItemAsync(orderItem);

                if (response?.IsSuccess == true)
                {
                    Snackbar.Add("Product added to cart!", Severity.Success);
                    CartService.NotifyCartChanged();
                }
                else
                {
                    Snackbar.Add(response?.Message ?? "Failed to add product to cart.", Severity.Error);
                }
            }
            catch
            {
                Snackbar.Add("An error occurred while adding to cart.", Severity.Error);
            }
        }
    }
}
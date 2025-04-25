using Microsoft.AspNetCore.Components;
using SalesSystem.UI.Services.Interfaces;
using SalesSystem.UI.ViewModels.Catalog;
using SalesSystem.UI.ViewModels.Responses;

namespace SalesSystem.UI.Components.Pages.Catalog
{
    public partial class HomePage : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public ICatalogService CatalogService { get; set; } = default!;

        public PagedResponseViewModel<CatalogViewModel?>? Response;
        public bool IsLoading = true;
        public const int DEFAULT_PAGE_SIZE = 8;
        public const int DEFAULT_PAGE = 1;

        protected override async Task OnInitializedAsync()
        {
            await LoadProductsAsync(DEFAULT_PAGE);
        }

        private async Task LoadProductsAsync(int page)
        {
            try
            {
                IsLoading = true;
                Response = await CatalogService.GetAllProductsAsync(page, DEFAULT_PAGE_SIZE);
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

        public async Task HandlePageChanged(int page)
        {
            await LoadProductsAsync(page);
        }

        public void ViewProduct(Guid productId)
        {
            NavigationManager.NavigateTo($"/details/{productId}");
        }
    }
}

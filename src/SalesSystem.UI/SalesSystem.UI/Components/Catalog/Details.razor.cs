using Microsoft.AspNetCore.Components;
using SalesSystem.UI.Services.Interfaces;
using SalesSystem.UI.ViewModels;

namespace SalesSystem.UI.Components.Catalog
{
    public partial class DetailsPage : ComponentBase
    {
        [Parameter]
        public Guid ProductId { get; set; }

        [Inject]
        private ICatalogService CatalogService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

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

        public void GoBack()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}

using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesSystem.UI.Services.Interfaces;
using SalesSystem.UI.ViewModels;

namespace SalesSystem.UI.Components.Pages.Sales
{
    public partial class CartPage : ComponentBase
    {
        [Inject]
        private ISalesService SalesService { get; set; } = default!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        public ResponseViewModel<OrderSummaryViewModel?>? Response;
        public bool IsLoading = true;
        public Dictionary<Guid, int> Quantities = [];
        public Dictionary<Guid, bool> IsUpdating = [];

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
                if (Response?.IsSuccess == true && Response.Data != null)
                {
                    foreach (var item in Response.Data.Items)
                    {
                        Quantities[item.ProductId] = item.Quantity;
                        IsUpdating[item.ProductId] = false;
                    }
                }
            }
            catch (Exception)
            {
                NavigationManager.NavigateTo("/error");
                Snackbar.Add("Something has failed during your Cart loading, try again later.", Severity.Error);
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

        public async Task UpdateQuantity(Guid productId)
        {
            if (!Quantities.TryGetValue(productId, out int quantity)) return;
            if (quantity < 1) return;

            IsUpdating[productId] = true;
            StateHasChanged();

            try
            {
                var updateModel = new UpdateOrderItemViewModel(productId, quantity);
                var result = await SalesService.UpdateOrderItemAsync(updateModel);
                if (result?.IsSuccess == true)
                {
                    await LoadCartSummaryAsync();
                    Snackbar.Add("Quantity updated successfully!", Severity.Success);
                }
                else
                {
                    Snackbar.Add("Failed to update quantity.", Severity.Error);
                }
            }
            catch (Exception)
            {
                Snackbar.Add("An error occurred while updating quantity.", Severity.Error);
            }
            finally
            {
                IsUpdating[productId] = false;
                StateHasChanged();
            }
        }

        public async Task DeleteItem(Guid productId)
        {
            IsUpdating[productId] = true;
            StateHasChanged();

            try
            {
                var result = await SalesService.RemoveOrderItemAsync(productId);
                if (result?.IsSuccess == true)
                {
                    await LoadCartSummaryAsync();
                    Snackbar.Add("Item removed from cart successfully!", Severity.Success);
                }
                else
                {
                    Snackbar.Add("Failed to remove item from cart.", Severity.Error);
                }
            }
            catch (Exception)
            {
                Snackbar.Add("An error occurred while removing the item.", Severity.Error);
            }
            finally
            {
                IsUpdating[productId] = false;
                StateHasChanged();
            }
        }
    }
}
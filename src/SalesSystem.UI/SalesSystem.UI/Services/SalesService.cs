using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SalesSystem.UI.Services.Interfaces;
using SalesSystem.UI.ViewModels;

namespace SalesSystem.UI.Services
{
    public sealed class SalesService(HttpClient httpClient, ProtectedLocalStorage protectedLocalStorage)
                      : Service(protectedLocalStorage), ISalesService
    {
        public async Task<ResponseViewModel?> AddOrderItemAsync(AddOrderItemViewModel orderItem)
        {
            await SetTokenAsync(httpClient).ConfigureAwait(false);
            var response = await httpClient.PostAsync("api/v1/sales/cart/item", GetContent(orderItem)).ConfigureAwait(false);
            return await DeserializeObjectResponse<ResponseViewModel?>(response).ConfigureAwait(false);
        }

        public async Task<ResponseViewModel<OrderSummaryViewModel?>?> GetOrderSummaryAsync()
        {
            await SetTokenAsync(httpClient).ConfigureAwait(false);
            var response = await httpClient.GetAsync("api/v1/sales/cart").ConfigureAwait(false);
            return await DeserializeObjectResponse<ResponseViewModel<OrderSummaryViewModel?>>(response).ConfigureAwait(false);
        }
    }
}

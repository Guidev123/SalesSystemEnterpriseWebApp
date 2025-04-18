using SalesSystemWebApp.ViewModels;

namespace SalesSystemWebApp.Services.Sales
{
    public class SalesService(IHttpClientFactory httpClientFactory, ICartService cartService) : Service, ISalesService
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient("SalesSystem");

        public async Task<ResponseViewModel<CreatedOrderItemViewModel?>?> AddOrderItemAsync(OrderItemViewModel orderItem)
        {
            var response = await _client.PostAsync("/api/v1/sales/cart/item", GetContent(orderItem)).ConfigureAwait(false);
            cartService.NotifyCartChanged();

            return await DeserializeObjectResponse<ResponseViewModel<CreatedOrderItemViewModel?>?>(response).ConfigureAwait(false);
        }

        public async Task<ResponseViewModel<CartViewModel?>?> GetCartAsync()
        {
            var response = await _client.GetAsync("/api/v1/sales/cart").ConfigureAwait(false);

            return await DeserializeObjectResponse<ResponseViewModel<CartViewModel?>?>(response).ConfigureAwait(false);
        }

    }
}

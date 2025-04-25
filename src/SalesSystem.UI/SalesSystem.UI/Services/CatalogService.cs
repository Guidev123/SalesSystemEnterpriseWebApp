using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SalesSystem.UI.Services.Interfaces;
using SalesSystem.UI.ViewModels;

namespace SalesSystem.UI.Services
{
    public sealed class CatalogService(HttpClient httpClient, ProtectedLocalStorage protectedLocalStorage)
                      : Service(protectedLocalStorage), ICatalogService
    {
        public async Task<PagedResponseViewModel<CatalogViewModel?>?> GetAllProductsAsync(int page, int pageSize)
        {
            var result = await httpClient.GetAsync($"api/v1/catalog?pageNumber={page}&pageSize={pageSize}").ConfigureAwait(false);
            return await DeserializeObjectResponse<PagedResponseViewModel<CatalogViewModel?>>(result).ConfigureAwait(false);
        }

        public async Task<ResponseViewModel<ProductDetailsViewModel?>?> GetProductByIdAsync(Guid productId)
        {
            var result = await httpClient.GetAsync($"api/v1/catalog/{productId}").ConfigureAwait(false);
            return await DeserializeObjectResponse<ResponseViewModel<ProductDetailsViewModel?>>(result).ConfigureAwait(false);
        }
    }
}

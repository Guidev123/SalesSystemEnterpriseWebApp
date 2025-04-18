using SalesSystemWebApp.ViewModels;

namespace SalesSystemWebApp.Services.Catalog
{
    public sealed class CatalogService(IHttpClientFactory httpClientFactory) 
                      : Service, ICatalogService
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient("SalesSystem");

        public async Task<PagedResponseViewModel<CatalogViewModel?>?> GetAllAsync(PagedRequestViewModel request)
        {
            var response = await _client.GetAsync($"/api/v1/catalog?pageNumber={request.PageNumber}&pageSize={request.PageSize}").ConfigureAwait(false);

            return await DeserializeObjectResponse<PagedResponseViewModel<CatalogViewModel?>?>(response);
        }
    }

}

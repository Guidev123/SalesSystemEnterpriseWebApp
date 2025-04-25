using SalesSystem.UI.ViewModels.Catalog;
using SalesSystem.UI.ViewModels.Responses;

namespace SalesSystem.UI.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<PagedResponseViewModel<CatalogViewModel?>?> GetAllProductsAsync(int page, int pageSize);
        Task<ResponseViewModel<ProductDetailsViewModel?>?> GetProductByIdAsync(Guid productId);
    }
}

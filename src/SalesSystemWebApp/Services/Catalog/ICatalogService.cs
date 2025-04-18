using SalesSystemWebApp.ViewModels;
using SalesSystemWebApp.ViewModels.Catalog;

namespace SalesSystemWebApp.Services.Catalog
{
    public interface ICatalogService
    {
        Task<PagedResponseViewModel<CatalogViewModel?>?> GetAllAsync(PagedRequestViewModel request);
    }
}

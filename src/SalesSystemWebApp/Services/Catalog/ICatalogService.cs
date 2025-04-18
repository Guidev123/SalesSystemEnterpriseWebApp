using SalesSystemWebApp.ViewModels;

namespace SalesSystemWebApp.Services.Catalog
{
    public interface ICatalogService
    {
        Task<PagedResponseViewModel<CatalogViewModel?>?> GetAllAsync(PagedRequestViewModel request);
    }
}

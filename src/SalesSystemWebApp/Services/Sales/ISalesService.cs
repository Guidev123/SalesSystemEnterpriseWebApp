using SalesSystemWebApp.ViewModels;
using static SalesSystemWebApp.Services.Sales.SalesService;

namespace SalesSystemWebApp.Services.Sales
{
    public interface ISalesService
    {
        Task<ResponseViewModel<CreatedOrderItemViewModel?>?> AddOrderItemAsync(OrderItemViewModel orderItem);
        Task<ResponseViewModel<CartViewModel?>?> GetCartAsync();
    }
}

using SalesSystemWebApp.ViewModels;
using SalesSystemWebApp.ViewModels.Sales;

namespace SalesSystemWebApp.Services.Sales
{
    public interface ISalesService
    {
        Task<ResponseViewModel<CreatedOrderItemViewModel?>?> AddOrderItemAsync(OrderItemViewModel orderItem);
        Task<ResponseViewModel<CartViewModel?>?> GetCartAsync();
        Task<ResponseViewModel<StartOrderResponseViewModel?>?> StartOrderAsync(CartItemViewModel cartItemViewModel);
    }
}

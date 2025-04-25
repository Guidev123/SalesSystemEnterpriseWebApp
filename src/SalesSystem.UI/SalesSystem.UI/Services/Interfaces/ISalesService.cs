using SalesSystem.UI.ViewModels;

namespace SalesSystem.UI.Services.Interfaces
{
    public interface ISalesService
    {
        Task<ResponseViewModel?> AddOrderItemAsync(AddOrderItemViewModel orderItem);
        Task<ResponseViewModel<OrderSummaryViewModel?>?> GetOrderSummaryAsync();
        Task<ResponseViewModel> UpdateOrderItemAsync(Guid orderItemId, int quantity);
    }
}

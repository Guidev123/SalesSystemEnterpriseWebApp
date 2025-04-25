using SalesSystem.UI.ViewModels;
using SalesSystem.UI.ViewModels.Responses;
using SalesSystem.UI.ViewModels.Sales;

namespace SalesSystem.UI.Services.Interfaces
{
    public interface ISalesService
    {
        Task<ResponseViewModel?> AddOrderItemAsync(AddOrderItemViewModel orderItem);
        Task<ResponseViewModel<OrderSummaryViewModel?>?> GetOrderSummaryAsync();
        Task<ResponseViewModel?> UpdateOrderItemAsync(UpdateOrderItemViewModel updateOrderItem);
        Task<ResponseViewModel?> RemoveOrderItemAsync(Guid productId);
        Task<ResponseViewModel?> ApplyVoucherAsync(VoucherViewModel voucher);
    }
}

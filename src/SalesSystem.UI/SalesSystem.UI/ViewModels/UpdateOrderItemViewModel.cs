namespace SalesSystem.UI.ViewModels
{
    public record UpdateOrderItemViewModel(Guid OrderId, Guid ProductId, int Quantity);
}

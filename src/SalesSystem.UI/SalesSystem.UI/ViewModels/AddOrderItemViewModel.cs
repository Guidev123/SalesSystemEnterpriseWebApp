namespace SalesSystem.UI.ViewModels
{
    public record AddOrderItemViewModel(Guid ProductId, string Name, int Quantity, decimal UnitPrice);
}

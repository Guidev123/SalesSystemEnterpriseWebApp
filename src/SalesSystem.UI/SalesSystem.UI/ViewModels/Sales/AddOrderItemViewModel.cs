namespace SalesSystem.UI.ViewModels.Sales
{
    public record AddOrderItemViewModel(Guid ProductId, string Name, int Quantity, decimal UnitPrice);
}

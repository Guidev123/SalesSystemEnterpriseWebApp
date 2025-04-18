namespace SalesSystemWebApp.ViewModels.Sales
{
    public record OrderItemViewModel(Guid ProductId, string Name, int Quantity, decimal UnitPrice);
}

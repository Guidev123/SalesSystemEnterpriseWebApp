namespace SalesSystemWebApp.ViewModels
{
    public record OrderItemViewModel(Guid ProductId, string Name, int Quantity, decimal UnitPrice);
}

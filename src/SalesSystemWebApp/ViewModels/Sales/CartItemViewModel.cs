namespace SalesSystemWebApp.ViewModels.Sales
{
    public record CartItemViewModel(
        Guid ProductId,
        string ProductName,
        int Quantity,
        decimal UnitPrice,
        decimal TotalPrice);
}

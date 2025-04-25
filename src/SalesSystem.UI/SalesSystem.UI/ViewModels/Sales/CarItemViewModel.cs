namespace SalesSystem.UI.ViewModels.Sales
{
    public record CarItemViewModel(Guid ProductId,
        string ProductName,
        int Quantity,
        decimal UnitPrice,
        decimal TotalPrice);
}

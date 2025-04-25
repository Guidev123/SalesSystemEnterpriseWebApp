namespace SalesSystem.UI.ViewModels
{
    public record CarItemViewModel(Guid ProductId,
        string ProductName,
        int Quantity,
        decimal UnitPrice,
        decimal TotalPrice);
}

namespace SalesSystem.UI.ViewModels.Sales
{
    public record OrderSummaryViewModel(Guid OrderId,
        decimal SubTotal,
        decimal TotalPrice,
        decimal TotalDiscount,
        string? VoucherCode,
        List<CarItemViewModel> Items);
}

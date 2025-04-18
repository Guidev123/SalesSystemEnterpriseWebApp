namespace SalesSystemWebApp.ViewModels
{
    public record CartViewModel(
        Guid OrderId,
        decimal SubTotal,
        decimal TotalPrice,
        decimal TotalDiscount,
        string? VoucherCode,
        List<CartItemViewModel> Items);
}

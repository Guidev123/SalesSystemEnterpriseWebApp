namespace SalesSystemWebApp.ViewModels
{
    public record ProductsViewModel(Guid Id, string Name, string Description, string Image,
        decimal Price, int QuantityInStock, decimal Height,
        decimal Width, decimal Depth, CategoryViewModel Category);

}

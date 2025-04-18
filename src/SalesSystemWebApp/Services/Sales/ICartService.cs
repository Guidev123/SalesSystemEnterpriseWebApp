namespace SalesSystemWebApp.Services.Sales
{
    public interface ICartService
    {
        void NotifyCartChanged();
        event Action? OnCartChanged;
    }
}

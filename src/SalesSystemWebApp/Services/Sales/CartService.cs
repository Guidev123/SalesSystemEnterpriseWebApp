
namespace SalesSystemWebApp.Services.Sales
{
    public class CartService : ICartService
    {
        public event Action? OnCartChanged;

        public void NotifyCartChanged()
        {
            OnCartChanged?.Invoke();
        }
    }
}

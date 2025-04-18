using SalesSystemWebApp.ViewModels;

namespace SalesSystemWebApp.Services.Register
{
    public interface IRegistersService
    {
        Task<ResponseViewModel<LoginResponseViewModel?>?> LoginAsync(LoginViewModel login);
        Task<ResponseViewModel<LoginResponseViewModel?>?> RegisterAsync(RegisterViewModel register);
        Task<ResponseViewModel<UserViewModel?>?> GetAsync();
    }
}

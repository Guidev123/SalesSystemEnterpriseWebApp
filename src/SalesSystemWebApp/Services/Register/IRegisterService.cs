using SalesSystemWebApp.Responses;
using SalesSystemWebApp.ViewModels;

namespace SalesSystemWebApp.Services.Register
{
    public interface IRegisterService
    {
        Task<Response<LoginResponseViewModel>> LoginAsync(LoginViewModel login);
        Task<Response<LoginResponseViewModel>> RegisterAsync(RegisterViewModel register);
        Task<Response<UserViewModel>> GetAsync();
    }
}

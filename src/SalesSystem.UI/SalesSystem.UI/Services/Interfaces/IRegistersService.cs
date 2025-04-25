using SalesSystem.UI.ViewModels;
using SalesSystem.UI.ViewModels.Responses;

namespace SalesSystem.UI.Services.Interfaces
{
    public interface IRegistersService
    {
        Task<ResponseViewModel<SignInResponseViewModel?>?> SignInAsync(SignInViewModel signInRequest);
        Task<ResponseViewModel?> SignUpAsync(SignUpViewModel signUpRequest);
    }

}

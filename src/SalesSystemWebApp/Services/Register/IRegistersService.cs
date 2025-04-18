using SalesSystemWebApp.ViewModels;
using SalesSystemWebApp.ViewModels.Registers;

namespace SalesSystemWebApp.Services.Register
{
    public interface IRegistersService
    {
        Task<ResponseViewModel<LoginResponseViewModel?>?> SignInAsync(SignInViewModel login);
        Task<ResponseViewModel<LoginResponseViewModel?>?> SignUpAsync(SignUpViewModel register);
        Task<ResponseViewModel<UserViewModel?>?> GetAsync();
        Task<ResponseViewModel<string?>?> AddAddressAsync(AddressViewModel address);
    }

    public record AddressViewModel(string Street, string Number, string AdditionalInfo,
                                 string Neighborhood, string ZipCode,
                                 string City, string State);
}

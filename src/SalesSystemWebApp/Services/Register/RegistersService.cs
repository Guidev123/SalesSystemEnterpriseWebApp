using SalesSystemWebApp.ViewModels;
using SalesSystemWebApp.ViewModels.Registers;

namespace SalesSystemWebApp.Services.Register
{
    public class RegistersService(IHttpClientFactory httpClientFactory)
                               : Service, IRegistersService
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient("SalesSystem");

        public async Task<ResponseViewModel<string?>?> AddAddressAsync(AddressViewModel address)
        {
            var response = await _client.GetAsync("/api/v1/registers/address").ConfigureAwait(false);

            return await DeserializeObjectResponse<ResponseViewModel<string?>?>(response);
        }

        public async Task<ResponseViewModel<UserViewModel?>?> GetAsync()
        {
            var response = await _client.GetAsync("/api/v1/registers").ConfigureAwait(false);

            return await DeserializeObjectResponse<ResponseViewModel<UserViewModel?>>(response);
        }

        public async Task<ResponseViewModel<UserSessionViewModel?>?> SignInAsync(SignInViewModel login)
        {
            var response = await _client.PostAsync("/api/v1/registers/signin", GetContent(login)).ConfigureAwait(false);

            return await DeserializeObjectResponse<ResponseViewModel<UserSessionViewModel?>>(response);
        }

        public async Task<ResponseViewModel<UserSessionViewModel?>?> SignUpAsync(SignUpViewModel register)
        {
            var response = await _client.PostAsync("/api/v1/registers", GetContent(register)).ConfigureAwait(false);

            return await DeserializeObjectResponse<ResponseViewModel<UserSessionViewModel?>>(response);
        }
    }
}

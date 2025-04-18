using SalesSystemWebApp.ViewModels;

namespace SalesSystemWebApp.Services.Register
{
    public class RegistersService(IHttpClientFactory httpClientFactory)
                               : Service, IRegistersService
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient("SalesSystem");

        public async Task<ResponseViewModel<UserViewModel?>?> GetAsync()
        {
            var response = await _client.GetAsync("/api/v1/registers").ConfigureAwait(false);

            return await DeserializeObjectResponse<ResponseViewModel<UserViewModel?>>(response);
        }

        public async Task<ResponseViewModel<LoginResponseViewModel?>?> LoginAsync(LoginViewModel login)
        {
            var response = await _client.PostAsync("/api/v1/registers/signin", GetContent(login)).ConfigureAwait(false);

            return await DeserializeObjectResponse<ResponseViewModel<LoginResponseViewModel?>>(response);
        }

        public async Task<ResponseViewModel<LoginResponseViewModel?>?> RegisterAsync(RegisterViewModel register)
        {
            var response = await _client.PostAsync("/api/v1/registers", GetContent(register)).ConfigureAwait(false);

            return await DeserializeObjectResponse<ResponseViewModel<LoginResponseViewModel?>>(response);
        }
    }
}

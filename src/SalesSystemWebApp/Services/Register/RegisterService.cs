using SalesSystemWebApp.Configurations;
using SalesSystemWebApp.Responses;
using SalesSystemWebApp.ViewModels;

namespace SalesSystemWebApp.Services.Register
{
    public class RegisterService(IHttpClientFactory httpClientFactory)
                                        : Service, IRegisterService
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient(WebConfiguration.HTTP_CLIENT_NAME);

        public async Task<Response<UserViewModel>> GetAsync()
        {
            var response = await _client.GetAsync("/api/v1/register").ConfigureAwait(false);

            var result = await DeserializeObjectResponse<Response<UserViewModel>>(response);

            return response.IsSuccessStatusCode
                ? new(result!.Data, 200, "Get user successfully") : new(null, 400, "Something failed during your login.", result!.Errors);
        }

        public async Task<Response<LoginResponseViewModel>> LoginAsync(LoginViewModel login)
        {
            var response = await _client.PostAsync("/api/v1/register/login", GetContent(login)).ConfigureAwait(false);

            var result = await DeserializeObjectResponse<Response<LoginResponseViewModel>>(response);

            return response.IsSuccessStatusCode
                ? new(result!.Data, 200, "Login successfully") : new(null, 400, "Something failed during your login.", result!.Errors);
        }

        public async Task<Response<LoginResponseViewModel>> RegisterAsync(RegisterViewModel register)
        {
            var response = await _client.PostAsync("/api/v1/register", GetContent(register)).ConfigureAwait(false);

            var result = await DeserializeObjectResponse<Response<LoginResponseViewModel>>(response);

            return response.IsSuccessStatusCode
                ? new(result!.Data, 201, "Login successfully") : new(null, 400, "Something failed during your login.", result!.Errors);
        }
    }
}

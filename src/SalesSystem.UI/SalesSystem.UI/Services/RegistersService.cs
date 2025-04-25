using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using SalesSystem.UI.Services.Interfaces;
using SalesSystem.UI.ViewModels;

namespace SalesSystem.UI.Services
{
    public sealed class RegistersService(HttpClient httpClient, ProtectedLocalStorage protectedLocalStorage)
                      : Service(protectedLocalStorage), IRegistersService
    {
        public async Task<ResponseViewModel<SignInResponseViewModel?>?> SignInAsync(SignInViewModel signInRequest)
        {
            var response = await httpClient.PostAsync("api/v1/registers/signin", GetContent(signInRequest)).ConfigureAwait(false);

            return await DeserializeObjectResponse<ResponseViewModel<SignInResponseViewModel?>>(response);
        }

        public Task<ResponseViewModel?> SignUpAsync(SignUpViewModel signUpRequest)
        {
            throw new NotImplementedException();
        }
    }
}

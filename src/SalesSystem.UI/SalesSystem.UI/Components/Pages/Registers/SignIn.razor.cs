using Microsoft.AspNetCore.Components;
using SalesSystem.UI.ViewModels;

namespace SalesSystem.UI.Components.Pages.Registers
{
    public partial class SignInPage : ComponentBase
    {
        public SignInViewModel InputModel { get; set; } = new();
        public bool IsBusy { get; set; } = false;

        protected async Task OnValidSubmitAsync()
        {

        }
    }
}

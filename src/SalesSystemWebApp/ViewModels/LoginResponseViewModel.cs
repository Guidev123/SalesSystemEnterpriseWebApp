namespace SalesSystemWebApp.ViewModels
{
    public record LoginResponseViewModel
    {
        public string AccessToken { get; set; } = string.Empty;
        public UserTokenViewModel UserToken { get; set; } = null!;
        public double ExpiresIn { get; set; }
    }
}

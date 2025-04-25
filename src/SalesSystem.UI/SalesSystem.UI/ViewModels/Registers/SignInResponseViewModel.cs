namespace SalesSystem.UI.ViewModels
{
    public record SignInResponseViewModel
    {
        public string AccessToken { get; set; } = string.Empty;
        public UserTokenViewModel UserToken { get; set; } = null!;
        public double ExpiresIn { get; set; }
        public DateTime ExpiryTimeStamp { get; set; }
    }
}

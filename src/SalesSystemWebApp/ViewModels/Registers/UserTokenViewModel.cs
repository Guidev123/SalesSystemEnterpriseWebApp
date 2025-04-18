namespace SalesSystemWebApp.ViewModels.Registers
{
    public record UserTokenViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public IReadOnlyCollection<ClaimViewModel> Claims { get; set; } = [];
        public IReadOnlyCollection<string> Roles { get; set; } = [];
    }
}

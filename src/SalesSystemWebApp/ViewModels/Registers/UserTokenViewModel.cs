namespace SalesSystemWebApp.ViewModels.Registers
{
    public record UserTokenViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public IEnumerable<ClaimViewModel> Claims { get; set; } = [];
    }
}

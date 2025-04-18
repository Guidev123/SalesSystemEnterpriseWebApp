namespace SalesSystemWebApp.ViewModels.Registers
{
    public record UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public IReadOnlyCollection<string> Roles { get; set; } = default!;
    }
}

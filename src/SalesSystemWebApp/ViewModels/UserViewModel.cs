namespace SalesSystemWebApp.ViewModels
{
    public record UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public IReadOnlyDictionary<string, string> Claims { get; set; } = default!;
    }
}

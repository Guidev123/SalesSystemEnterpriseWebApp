namespace SalesSystem.UI.ViewModels.Responses
{
    public record ResponseViewModel(bool IsSuccess, ICollection<string> Errors, string? Message);
    public record ResponseViewModel<T>(T Data, bool IsSuccess, ICollection<string> Errors, string? Message);
}

namespace SalesSystem.UI.ViewModels
{
    public record PagedResponseViewModel<T>(T Data, bool IsSuccess, ICollection<string> Errors, string? Message, int TotalCount, int TotalPages, int CurrentPage, int PageSize);
}

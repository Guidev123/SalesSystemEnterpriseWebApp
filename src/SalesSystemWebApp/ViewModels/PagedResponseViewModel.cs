namespace SalesSystemWebApp.ViewModels
{
    public record PagedResponseViewModel<T>
    {
        public PagedResponseViewModel(T? data, bool isSuccess, List<string>? errors, string? message, int totalCount, int totalPages, int currentPage)
        {
            Data = data;
            IsSuccess = isSuccess;
            Errors = errors;
            Message = message;
            TotalCount = totalCount;
            TotalPages = totalPages;
            CurrentPage = currentPage;
        }

        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public List<string>? Errors { get; set; } = [];
        public string? Message { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}

namespace SalesSystemWebApp.ViewModels
{
    public record ResponseViewModel<T>
    {
        public ResponseViewModel(T? data, bool isSuccess, List<string>? errors, string? message, int statusCode)
        {
            Data = data;
            IsSuccess = isSuccess;
            Errors = errors;
            StatusCode = statusCode;
            Message = message;
        }

        public T? Data { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string>? Errors { get; set; } = [];
        public string? Message { get; set; }
    }
}

namespace MrKatsuWebAPI.DTO.ApiResponse
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public string[] ValidationErrors { get; set; } = Array.Empty<string>();
        public ApiErrorResult()
        {
        }
        public ApiErrorResult(string message, string[] validationErrors)
        {
            Success = false;
            Message = message;
            ValidationErrors = validationErrors;
        }
        public ApiErrorResult(string message)
        {
            Success = false;
            Message = message;
            ValidationErrors = Array.Empty<string>();
        }
        public ApiErrorResult(string[] validationErrors)
        {
            Success = false;
            ValidationErrors = validationErrors;
        }
    }
}

namespace MrKatsuWebAPI.DTO.ApiResponse
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public List<string> ValidationErrors { get; set; } = new List<string>();
        public ApiErrorResult()
        {
        }
        public ApiErrorResult(string message, List<string> validationErrors)
        {
            Success = false;
            Message = message;
            ValidationErrors = validationErrors;
        }
        public ApiErrorResult(string message)
        {
            Success = false;
            Message = message;
            ValidationErrors = new List<string>();
        }
        public ApiErrorResult(List<string> validationErrors)
        {
            Success = false;
            ValidationErrors = validationErrors;
        }
    }
}

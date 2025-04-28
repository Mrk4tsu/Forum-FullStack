namespace MrKatsuWebAPI.DTO.ApiResponse
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }
    }
}

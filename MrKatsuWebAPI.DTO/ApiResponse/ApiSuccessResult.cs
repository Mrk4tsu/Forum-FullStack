namespace MrKatsuWebAPI.DTO.ApiResponse
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T data)
        {
            Success = true;
            Data = data;
        }
        public ApiSuccessResult(T data, string message)
        {
            Success = true;
            Data = data;
            Message = message;
        }
        public ApiSuccessResult()
        {
            Success = true;
        }
    }
}

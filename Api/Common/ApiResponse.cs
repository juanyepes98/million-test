namespace Api.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Content { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; } = "OK";

    public ApiResponse() { }

    public ApiResponse(T content, int statusCode = 200, string message = "OK")
    {
        Success = true;
        Content = content;
        StatusCode = statusCode;
        Message = message;
    }

    public ApiResponse(string message, int statusCode = 400)
    {
        Success = false;
        Content = default;
        StatusCode = statusCode;
        Message = message;
    }

    public static ApiResponse<T> SuccessResponse(T content, int statusCode = 200, string message = "OK")
        => new(content, statusCode, message);

    public static ApiResponse<T> ErrorResponse(string message, int statusCode = 400)
        => new(message, statusCode);
}
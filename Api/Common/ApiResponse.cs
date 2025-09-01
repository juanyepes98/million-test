namespace Api.Common;

/// <summary>
/// Standard API response wrapper for returning data, status, and messages.
/// </summary>
/// <typeparam name="T">Type of the content returned by the API.</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Indicates whether the API request was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The content returned by the API. Null if the request failed.
    /// </summary>
    public T? Content { get; set; }

    /// <summary>
    /// HTTP status code of the response.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Message for the user describing the response.
    /// Defaults to "OK" for successful responses.
    /// </summary>
    public string Message { get; set; } = "OK";

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ApiResponse()
    {
    }

    /// <summary>
    /// Constructor for a successful response.
    /// </summary>
    /// <param name="content">Content of the response.</param>
    /// <param name="statusCode">HTTP status code (default 200).</param>
    /// <param name="message">Optional success message (default "OK").</param>
    public ApiResponse(T content, int statusCode = 200, string message = "OK")
    {
        Success = true;
        Content = content;
        StatusCode = statusCode;
        Message = message;
    }

    /// <summary>
    /// Constructor for an error response.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="statusCode">HTTP status code (default 400).</param>
    public ApiResponse(string message, int statusCode = 400)
    {
        Success = false;
        Content = default;
        StatusCode = statusCode;
        Message = message;
    }

    /// <summary>
    /// Creates a successful API response.
    /// </summary>
    /// <param name="content">Content of the response.</param>
    /// <param name="statusCode">HTTP status code (default 200).</param>
    /// <param name="message">Optional success message (default "OK").</param>
    /// <returns>An instance of <see cref="ApiResponse{T}"/> representing success.</returns>
    public static ApiResponse<T> SuccessResponse(T content, int statusCode = 200, string message = "OK")
        => new(content, statusCode, message);

    /// <summary>
    /// Creates an error API response.
    /// </summary>
    /// <param name="message">Error message.</param>
    /// <param name="statusCode">HTTP status code (default 400).</param>
    /// <returns>An instance of <see cref="ApiResponse{T}"/> representing an error.</returns>
    public static ApiResponse<T> ErrorResponse(string message, int statusCode = 400)
        => new(message, statusCode);
}
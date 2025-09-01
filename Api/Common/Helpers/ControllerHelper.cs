using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Common.Helpers;

/// <summary>
/// Helper class for handling controller actions in a consistent way.
/// Wraps API responses and standardizes exception handling.
/// </summary>
public static class ControllerHelper
{
    /// <summary>
    /// Executes an asynchronous action and wraps the result in a standardized <see cref="ApiResponse{T}"/>.
    /// Handles common exceptions like <see cref="KeyNotFoundException"/> and general <see cref="Exception"/>.
    /// </summary>
    /// <typeparam name="T">The type of the response content.</typeparam>
    /// <param name="action">A function that returns a task with the response content.</param>
    /// <param name="successMessage">Optional message for successful response (default: "OK").</param>
    /// <returns>An <see cref="IActionResult"/> containing a standardized API response.</returns>
    public static async Task<IActionResult> HandleRequestAsync<T>(
        Func<Task<T>> action,
        string successMessage = "OK")
    {
        try
        {
            // Execute the action and return a successful response
            var result = await action();
            return new OkObjectResult(ApiResponse<T>.SuccessResponse(result, 200, successMessage));
        }
        catch (KeyNotFoundException ex)
        {
            // Return 404 with standardized error response
            return new NotFoundObjectResult(ApiResponse<string>.ErrorResponse(ex.Message));
        }
        catch (Exception ex)
        {
            // Return 400 with standardized error response
            return new BadRequestObjectResult(ApiResponse<string>.ErrorResponse(ex.Message));
        }
    }
}

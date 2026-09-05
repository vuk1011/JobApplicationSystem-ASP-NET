using JobApplicationAPI.Common.Exceptions;
using JobApplicationAPI.DTOs;

namespace JobApplicationAPI.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, message) = exception switch
            {
                BadRequestException => (StatusCodes.Status400BadRequest, exception.Message),
                UnauthorizedException => (StatusCodes.Status401Unauthorized, exception.Message),
                ResourceNotFoundException => (StatusCodes.Status404NotFound, exception.Message),
                ConflictException => (StatusCodes.Status409Conflict, exception.Message),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
            };

            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                _logger.LogError(exception, "Unhandled exception occurred");
            }
            else
            {
                _logger.LogWarning(exception, "Handled exception: {Message}", exception.Message);
            }

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var responseMessage = statusCode == StatusCodes.Status500InternalServerError
                ? "An unexpected error occurred."
                : message;

            await context.Response.WriteAsJsonAsync(new ApiResponse(responseMessage));
        }
    }
}

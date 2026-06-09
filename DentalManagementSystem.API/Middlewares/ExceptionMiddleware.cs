using DentalManagementSystem.Application.DTO.Responses;
using DentalManagementSystem.Application.Exceptions;
using System.Net;

namespace DentalManagementSystem.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

                if (!httpContext.Response.HasStarted)
                {
                    if (httpContext.Response.StatusCode == (int)HttpStatusCode.Forbidden)
                    {
                        await WriteErrorResponse(httpContext, HttpStatusCode.Forbidden, "You do not have permission to access this resource.");
                    }
                    else if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                    {
                        await WriteErrorResponse(httpContext, HttpStatusCode.Unauthorized, "Authentication is required to access this resource.");
                    }
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (statusCode, errors, displayMessage) = exception switch
            {
                BadRequestException => (HttpStatusCode.BadRequest, null, exception.Message),
                ValidationException valEx => (HttpStatusCode.BadRequest, (object)valEx.Errors, valEx.Message),
                NotFoundException => (HttpStatusCode.NotFound, null, exception.Message),
                UnauthorizeException => (HttpStatusCode.Unauthorized, null, exception.Message),

                _ => (HttpStatusCode.InternalServerError, null, $"An unexpected error occurred. {exception}")
            };

            await WriteErrorResponse(context, statusCode, displayMessage, errors);
        }

        private async Task WriteErrorResponse(HttpContext context, HttpStatusCode statusCode, string message, object? errors = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = new BaseApiResponse<object>
            {
                StatusCode = (int)statusCode,
                Success = false,
                Data = errors,
                ErrorMessage = message,
            };

            await context.Response.WriteAsJsonAsync(result);
        }
    }
}

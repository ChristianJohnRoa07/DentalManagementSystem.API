using DentalManagementSystem.Application.DTO.Responses;
using DentalManagementSystem.Identity.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.API.Middlewares
{
    public class BlacklistTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public BlacklistTokenMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context, ApplicationUserDbContext dbContext)
        {
            var authHeader = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();

                // Check if the token exists in your new BlacklistedTokens table
                var isBlacklisted = await dbContext.BlacklistedTokens
                    .AnyAsync(t => t.Token == token);

                if (isBlacklisted)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    var result = new BaseApiResponse<object>
                    {
                        StatusCode = StatusCodes.Status401Unauthorized,
                        Success = false,
                        ErrorMessage = "Token has been revoked. Please log in again."
                    };

                    await context.Response.WriteAsJsonAsync(result);
                    return;
                }
            }

            await _next(context);
        }
    }
}

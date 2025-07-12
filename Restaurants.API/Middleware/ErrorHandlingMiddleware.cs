
namespace Restaurants.API.Middleware;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> _logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await  next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError("Something went wrong:{Message}",ex.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new
            {
                statusCode = context.Response.StatusCode,
                message = ex.Message
            });

        }

    }
}

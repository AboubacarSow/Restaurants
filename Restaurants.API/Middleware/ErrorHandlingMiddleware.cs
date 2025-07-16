
using Restaurants.Domain.Exceptions;

namespace Restaurants.API.Middleware;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> _logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await  next.Invoke(context);
        }
        catch(NotFoundException notFoud)
        {
            _logger.LogWarning("Resource not found:{@Resource}",notFoud.Message);
            context.Response.StatusCode=StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new
            {
                statusCode = context.Response.StatusCode,
                message = notFoud.Message
            });
        }
        catch (ForbidenException forbiden)
        {
            _logger.LogWarning("{Message}", forbiden.Message);
           context.Response.StatusCode=StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new
            {
                statusCode = context.Response.StatusCode,
                message = forbiden.Message
            });
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

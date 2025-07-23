using System.Diagnostics;

namespace Restaurants.API.Middleware;

public class RequestTimeLoggingMiddleware : IMiddleware
{
    private readonly ILogger<RequestTimeLoggingMiddleware> _logger;

    public RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopwatch = Stopwatch.StartNew();
        await next(context);
        stopwatch.Stop();
        

        if(stopwatch.ElapsedMilliseconds /1000 > 4)
        {
            _logger.LogInformation("Request {[Verb]} at {Path} took {Time} ms",
                context.Request.Method,
                context.Request.Path,
                stopwatch.ElapsedMilliseconds);

        }
    }
}
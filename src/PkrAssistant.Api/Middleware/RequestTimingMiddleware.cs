using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PkrAssistant.Api.Middleware;

internal class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimingMiddleware> _logger;

    public RequestTimingMiddleware(
        RequestDelegate next, 
        ILogger<RequestTimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var timer = Stopwatch.StartNew();

        await _next(context);

        var elapsed = timer.Elapsed;
        var statusCode = context.Response.StatusCode;

        var method = context.Request.Method;
        var path = context.Request.Path;

        _logger.LogInformation("""
            {Method} => "{Path}" returns {StatusCode}, elapsed {Elapsed} 
            
            """, 
            method, 
            path, 
            statusCode,
            elapsed);

    }
}

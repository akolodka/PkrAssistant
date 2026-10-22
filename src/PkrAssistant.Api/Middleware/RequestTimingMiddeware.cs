using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PkrAssistant.Api.Middleware;

internal class RequestTimingMiddeware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimingMiddeware> _logger;

    public RequestTimingMiddeware(
        RequestDelegate next, 
        ILogger<RequestTimingMiddeware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var timer = Stopwatch.StartNew();

        await _next(context);

        var elapsed = timer.Elapsed;

        var method = context.Request.Method;
        var path = context.Request.Path;

        _logger.Log(
            LogLevel.Information, 
            "Метод запроса: {Method}, путь запроса: {Path}, затрачено времени: {Elapsed}", 
            method, path, elapsed);

    }
}

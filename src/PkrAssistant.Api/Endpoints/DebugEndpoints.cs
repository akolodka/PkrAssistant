using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using PkrAssistant.Api.Extensions;
using System;

namespace PkrAssistant.Api.Endpoints;

public static class DebugEndpoints
{
    private const string RouteGroup = "api/debug";

    public static void MapUtcEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(RouteGroup);

        group.MapGet("/utc", (ILoggerFactory factory, HttpContext context) =>
        {
            // хороший способ установить категорию записи => фильтры проходят => лог виден
            var logger = factory.CreateLogger(typeof(DebugEndpoints));

            var path = context.Request.Path;

            var requestMethod = context.Request.Method;

            // пустая строка в выводе указана намеренно, разделяет записи для человека
            logger.LogInformation("""
                 {Method} => "{Path}", текущее время UTC {Time}
        
                 """,
                 requestMethod,
                 path,
                 DateTime.UtcNow.AsRuTime());
        });
    }
}

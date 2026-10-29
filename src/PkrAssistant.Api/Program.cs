using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PkrAssistant.Api.Extensions;
using PkrAssistant.Api.Middleware;
using PkrAssistant.Infrastructure;
using System;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerConfiguration();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestTimingMiddleware>();

app.MapGet("/timing", (ILogger<Program> logger) =>
{
    logger.LogInformation("""
        GET => "/timing" at {Time}
        
        """, 
        DateTime.UtcNow.AsRuTime());
});

app.Run();

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using PkrAssistant.Api.Endpoints;
using PkrAssistant.Api.Extensions;
using PkrAssistant.Api.Middleware;
using PkrAssistant.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerConfiguration();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapUtcEndpoint();
}

app.UseMiddleware<RequestTimingMiddleware>();

app.Run();

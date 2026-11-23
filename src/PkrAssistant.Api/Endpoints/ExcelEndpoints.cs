using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.IO;

namespace PkrAssistant.Api.Endpoints;

public static class ExcelEndpoints
{
    private const string RouteGroup = "api/excel";

    public static void MapExcelEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup(RouteGroup);

        group.MapGet("/file", () =>
        {
            // Временно для проверки работы эндпоинта. 
            // после подключение настоящего ассемблера логика будет в Infrastructure
            var filePath = Path.Combine(
                AppContext.BaseDirectory, 
                "TEMP", "example.xlsx");

            byte[] fileContent = File.ReadAllBytes(filePath);

            return Results.File(
                fileContent,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "result.xlsx");
        });
    }
}

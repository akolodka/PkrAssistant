using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace PkrAssistant.Api.Extensions;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "PkrAssistant.Api",
                Version = "v1",
                Description = "Api для автоматизации поверочных работ",

                Contact = new OpenApiContact
                {
                    Name = "Александр Колодка",
                    Email = "akolodka@rambler.ru"
                }
            });

            // Получить XML-комментарии из файла документации
            var xmlFilename = $"{typeof(SwaggerConfiguration).Assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

            options.IncludeXmlComments(xmlPath);
        });

        return services;
    }
}

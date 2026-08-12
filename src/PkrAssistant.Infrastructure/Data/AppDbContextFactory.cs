using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;

namespace PkrAssistant.Infrastructure.Data;

internal sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private readonly string DefaultConnectionName = "DefaultConnection";

    private readonly string EnvironmentVariableName = "DESIGN_TIME_CONNECTION";

    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()

            // указание на тип текущей сборки для поиска Guid секретов
            .AddUserSecrets<AppDbContextFactory>(optional: true)

            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString(DefaultConnectionName);

        // попытка прочитать настоящую строку подключения из среды окружения,
        // строка может быть передана через терминал
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString = Environment.GetEnvironmentVariable(EnvironmentVariableName);
        }

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                $"Connection string не найдена. " +
                $"Задайте её одним из способов:\n" +
                $"1. User Secrets (рекомендуется):\n" +
                $"   dotnet user-secrets set \"ConnectionStrings:{DefaultConnectionName}\" \"your_connection_string\" --project src/PkrAssistant.Infrastructure/PkrAssistant.Infrastructure.csproj\n" +
                $"2. Environment variable:\n" +
                $"   setx {EnvironmentVariableName} \"your_connection_string\"");
            ;
        }

        var builder = new DbContextOptionsBuilder<AppDbContext>();

        builder.UseNpgsql(connectionString);

        var context = new AppDbContext(builder.Options);

        return context;
    }
}

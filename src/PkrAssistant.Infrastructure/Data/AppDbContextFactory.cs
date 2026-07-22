using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace PkrAssistant.Infrastructure.Data;

internal sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // попытка прочитать настоящую строку подклюения из среды окружения,
        // строка будет передана через терминал
        var connectionString = Environment.GetEnvironmentVariable("DESIGN_TIME_CONNECTION");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Переменная окружения DESIGN_TIME_CONNECTION не задана. " +
                "Задайте её один раз командой: " +
                "setx DESIGN_TIME_CONNECTION \"Host=localhost;Port=5432;Database=pkrassistant;Username=...;Password=...\" " +
                "и перезапустите Visual Studio.");
            ;
        }

        var builder = new DbContextOptionsBuilder<AppDbContext>();

        builder.UseNpgsql(connectionString);

        var context = new AppDbContext(builder.Options);

        return context;
    }
}

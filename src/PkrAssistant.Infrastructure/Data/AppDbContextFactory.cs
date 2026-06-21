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

        if (string.IsNullOrWhiteSpace(connectionString) == true)
        {
            connectionString = "Host=shub_host;Port=5432;Database=stub_db;Username=stub_user;Password=stub_password";
        }

        var builder = new DbContextOptionsBuilder<AppDbContext>();

        builder.UseNpgsql(connectionString);

        return new AppDbContext(builder.Options);
    }
}
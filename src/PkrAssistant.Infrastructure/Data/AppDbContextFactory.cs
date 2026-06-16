using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PkrAssistant.Infrastructure.Data;

internal sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var apiPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "PkrAssistant.Api");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(apiPath)
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var builder = new DbContextOptionsBuilder<AppDbContext>();

        builder.UseNpgsql(connectionString);

        return new AppDbContext(builder.Options);
    }
}

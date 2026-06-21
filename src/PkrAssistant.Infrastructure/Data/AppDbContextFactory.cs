using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PkrAssistant.Infrastructure.Data;

internal sealed class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var connectionString = "Host=shub_host;Port=5432;Database=stub_db;Username=stub_user;Password=stub_password";

        var builder = new DbContextOptionsBuilder<AppDbContext>();

        builder.UseNpgsql(connectionString);

        return new AppDbContext(builder.Options);
    }
}
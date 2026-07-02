using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PkrAssistant.Application.Repositories;
using PkrAssistant.Infrastructure.Data;
using PkrAssistant.Infrastructure.Repositories;

namespace PkrAssistant.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IVerifierRepository, VerifierRepository>();

        return services;
    }
}

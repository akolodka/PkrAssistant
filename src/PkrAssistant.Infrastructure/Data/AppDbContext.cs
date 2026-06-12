using Microsoft.EntityFrameworkCore;
using PkrAssistant.Infrastructure.Data.Configurations;

namespace PkrAssistant.Infrastructure.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new VerifierConfiguration);
    }
}
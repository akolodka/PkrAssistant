using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PkrAssistant.Infrastructure.Data.Entities;

namespace PkrAssistant.Infrastructure.Data.Configurations;

internal class VerifierConfiguration : IEntityTypeConfiguration<VerifierEntity>
{
    public void Configure(EntityTypeBuilder<VerifierEntity> builder) 
    {
        builder.ToTable("verifiers");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(200);

        // Отчество не обязательно
        builder.Property(e => e.Patronymic)
            .HasMaxLength(200);
        
        builder.Property(e => e.Position)
            .IsRequired()
            .HasMaxLength(200);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PkrAssistant.Infrastructure.Data.Entities;

namespace PkrAssistant.Infrastructure.Data.Configurations;

/// <summary>
/// Конфигурация EF Core сущности ProtocolForm
/// </summary>
internal class ProtocolFormConfiguration : IEntityTypeConfiguration<ProtocolFormEntity>
{
    public void Configure(EntityTypeBuilder<ProtocolFormEntity> builder) 
    {
        builder.ToTable("protocol_forms");

        builder.HasKey(e => e.Id);

        // Для ускорения поиска по имени
        builder.HasIndex(e => e.Name);

        builder.Property(e => e.Name)
            .IsRequired() // Только для ссылочных (nullable) типов
            .HasMaxLength(200);

        builder.Property(e => e.TemplateFileId);

        // Новые формы активны по умолчанию.
        builder.Property(e => e.IsActive)
            .HasDefaultValue(true);

        // Использовать .HasDefaultValueSql("now()"), чтобы в базе была метка времени на момент создания сущности
        // если передать DateTime.Date, то фиксируется дата на момент миграции
        builder.Property(e => e.CreatedAt)
            .HasDefaultValueSql("now()"); 
        
        builder.Property(e => e.UpdatedAt)
            .HasDefaultValueSql("now()");
    }
}

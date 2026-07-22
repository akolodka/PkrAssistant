using System;

namespace PkrAssistant.Infrastructure.Data.Entities;

/// <summary>
/// Persistense-модель формы протокола поверки для EF Core
/// </summary>
internal class ProtocolFormEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public Guid TemplateFileId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}

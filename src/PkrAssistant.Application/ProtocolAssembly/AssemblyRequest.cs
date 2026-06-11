using System;
using System.Collections.Generic;

namespace PkrAssistant.Application.ProtocolAssembly;

/// <summary>
/// Инвариантный контейнер для сборки шаблона поверки.
/// </summary>
public record AssemblyRequest
{
    /// <summary>
    /// Идентификатор собираемого шаблона документа (протокол, свидетельство и т.д.).
    /// </summary>
    public Guid AssemblyId { get; init; }

    /// <summary>
    /// Список частей шаблона для сборки.
    /// </summary>
    public IReadOnlyList<Guid> TemplatePartIds { get; init; }

    public AssemblyRequest(
        Guid assemblyId, 
        IReadOnlyList<Guid> templatePartIds)
    {
        AssemblyId = assemblyId;
        TemplatePartIds = templatePartIds;
    }
}
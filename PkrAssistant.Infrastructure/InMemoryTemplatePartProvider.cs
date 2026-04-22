using PkrAssistant.Application.ProtocolAssembly;
using PkrAssistant.Domain.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PkrAssistant.Infrastructure;

/// <summary>
/// Хранит части шаблоны в памяти (для тестов и прототипирования).
/// </summary>
public class InMemoryTemplatePartProvider : ITemplatePartProvider
{
    private readonly Dictionary<Guid, TemplatePart> _store = new();

    /// <summary>
    /// Добавляет составные части шаблона в общий список.
    /// </summary>
    /// <param name="part">Часть шаблона для добавления.</param>
    public void AddPart(TemplatePart part)
    {
        // Удобнее для отладки
        _store[part.Id] = part;
    }

    public Task<IReadOnlyList<TemplatePart>> GetPartsByIdsAsync(IReadOnlyList<Guid> templatePartIds) 
    {
        IReadOnlyList<TemplatePart> result = _store
            .Where(p => templatePartIds.Contains(p.Key))
            .Select(p => p.Value)
            .ToList();

        return Task.FromResult(result);
    }
}
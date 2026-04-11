using PkrAssistant.Domain.Templates;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PkrAssistant.Application.ProtocolAssembly;

/// <summary>
/// Контракт на получение частей шаблона поверки из хранилища данных.
/// </summary>
public interface ITemplatePartProvider
{
    /// <summary>
    /// Загружает части шаблона по их идентификаторам.
    /// </summary>
    /// <param name="templatePartIds">Список идентификаторов частей шаблона поверки.</param>
    /// <returns>Список частей шаблона поверки.</returns>
    Task<IReadOnlyList<TemplatePart>> GetPartsByIdsAsync(IReadOnlyList<Guid> templatePartIds);
}
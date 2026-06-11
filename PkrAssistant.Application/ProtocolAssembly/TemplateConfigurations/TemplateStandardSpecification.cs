using System;

namespace PkrAssistant.Application.TemplateConfigurations;

/// <summary>
/// Конфигурация, описывающая связь между эталонами и конкретным шаблоном.
/// </summary>
public class TemplateStandardSpecification
{
    public Guid Id { get; private set; }

    /// <summary>
    /// Идентификатор части шаблона протокола, где содержатся сведения об эталоне.
    /// </summary>
    public Guid NeckTemplatePartId { get; private set; }

    /// <summary>
    /// Идентификатор комбинации единицы и разряда эталона.
    /// </summary>
    public Guid UnitRankCombinationId { get; private set; }

    /// <summary>
    /// Предпочитаемый эталон единицы величины (может быть пустым).
    /// </summary>
    public Guid? PreferredStandardId { get; private set; }

    /// <summary>
    /// Порядковый номер в шаблоне (от 1 до 10).
    /// </summary>
    public int OrderIndex { get; private set; }
    
    // Для EF
    private TemplateStandardSpecification() {}

    public TemplateStandardSpecification(
        Guid neckTemplatePartId, 
        Guid unitRankCombinationId,
        int orderIndex, 
        Guid? preferredStandardId)
    {
        if (neckTemplatePartId == Guid.Empty)
        {
            throw new ArgumentException("Идентификатор части шаблона протокола со сведениями об эталонах должен быть указан", nameof(neckTemplatePartId));
        }

        if (unitRankCombinationId == Guid.Empty)
        {
            throw new ArgumentException("Идентификатор комбинации единицы и разряда эталона должен быть указан", nameof(unitRankCombinationId));
        }

        if (orderIndex < 1 || orderIndex > 10)
        {
            throw new ArgumentException("Порядковый номер в шаблоне должен быть в диапазоне от 1 до 10", nameof(orderIndex));
        }

        if (preferredStandardId.HasValue == true && preferredStandardId.Value == Guid.Empty)
        {
            throw new ArgumentException("Идентификатор предпочитаемого эталона единицы величины должен быть указан", nameof(preferredStandardId));
        }

        Id = Guid.NewGuid();
        NeckTemplatePartId = neckTemplatePartId;

        UnitRankCombinationId = unitRankCombinationId;
        OrderIndex = orderIndex;

        PreferredStandardId = preferredStandardId;
    }

    public override string ToString()
    {
        return $"Шаблон: {NeckTemplatePartId}, комбинация: {UnitRankCombinationId}, порядковый номер: {OrderIndex}";
    }
}
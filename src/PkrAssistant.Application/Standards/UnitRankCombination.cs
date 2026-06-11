using System;

namespace PkrAssistant.Application.Standards;

/// <summary>
/// Комбинация единица измерений - разряд эталона.
/// </summary>
public class UnitRankCombination
{
    public Guid Id { get; private set; }
    
    /// <summary>
    /// Идентификатор единицы измерений.
    /// </summary>
    public Guid UnitOfMeasurementId { get; private set; }

    /// <summary>
    /// Идентификатор ранга эталона.
    /// </summary>
    public Guid StandardRankId { get; private set; }

    // Для EF
    private UnitRankCombination() {}

    public UnitRankCombination(Guid unitOfMeasurementId, Guid standardRankId)
    {
        if (unitOfMeasurementId == Guid.Empty)
        {
            throw new ArgumentException("Идентификатор единицы измерений должен быть указан", nameof(unitOfMeasurementId));
        }

        if (standardRankId == Guid.Empty)
        {
            throw new ArgumentException("Идентификатор ранга эталона должен быть указан", nameof(standardRankId));
        }

        Id = Guid.NewGuid();

        UnitOfMeasurementId = unitOfMeasurementId;
        StandardRankId = standardRankId;
    }

    public override string ToString()
    {
        return $"Combination {Id}";
    }
}
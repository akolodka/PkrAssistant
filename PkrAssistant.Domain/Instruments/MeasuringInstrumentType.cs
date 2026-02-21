using System;
using System.Collections.Generic;

namespace PkrAssistant.Domain.Instruments;

/// <summary>
/// Тип средства измерений
/// </summary>
public class MeasuringInstrumentType
{
    public Guid Id { get; private set; }
    public string TypeName { get; private set; }

    // Навигационное свойство
    public ICollection<ApprovedMeasuringInstrumentType> ApprovedTypes { get; private set; } 

    // Для EF
    private MeasuringInstrumentType(){}

    public MeasuringInstrumentType(string typeName)
    {
        if (string.IsNullOrWhiteSpace(typeName) == true)
        {
            throw new ArgumentException("Наименование типа СИ не может быть пустым", nameof(typeName));
        }

        Id = Guid.NewGuid();
        TypeName = typeName.Trim();

        ApprovedTypes = new List<ApprovedMeasuringInstrumentType>();
    }
}

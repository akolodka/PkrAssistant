using System;

namespace PkrAssistant.Application.Standards;

/// <summary>
/// Единица измерений физической величины.
/// </summary>
public class UnitOfMeasurement
{
    public Guid Id { get; private set; }

    /// <summary>
    /// Наименование единицы измерений.
    /// </summary>
    public string Name { get; private set; }

    // Для EF
    private UnitOfMeasurement() {}

    public UnitOfMeasurement(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Наименование единицы величины не может быть пустым", nameof(name));
        }

        Id = Guid.NewGuid();
        Name = name.Trim();
    }
}

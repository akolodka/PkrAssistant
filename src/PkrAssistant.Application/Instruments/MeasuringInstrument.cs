using System;

namespace PkrAssistant.Application.Instruments;

/// <summary>
/// Конкретное средство измерений.
/// </summary>
public class MeasuringInstrument
{
    public Guid Id { get; private set; }

    /// <summary>
    /// Заводской номер средства измерений.
    /// </summary>
    public string SerialNumber { get; private set; }

    /// <summary>
    /// Идентификатор утверждённого типа средства измерений.
    /// </summary>
    public Guid ApprovedMeasuringInstrumentTypeId { get; private set; }

    /// <summary>
    /// Наименование модификации средства измерений.
    /// </summary>
    public string? ModificationName { get; private set; }

    /// <summary>
    /// Состав средства измерений (измерительные блоки).
    /// </summary>
    public string? Composition { get; private set; }

    /// <summary>
    /// Межповерочный интервал в годах.
    /// </summary>
    public int VerificationIntervalYears { get; private set; }

    // Для EF
    private MeasuringInstrument() {}

    public MeasuringInstrument(
        string serialNumber, 
        Guid approvedMeasuringInstrumentTypeId,
        int verificationIntervalYears,
        string? modificationName = null,
        string? composition = null)
    {
        if (string.IsNullOrWhiteSpace(serialNumber))
        {
            throw new ArgumentException("Заводской номер не может быть пустым", nameof(serialNumber));
        }

        if (approvedMeasuringInstrumentTypeId == Guid.Empty)
        {
            throw new ArgumentException("Идентификатор утрверждённого типа средств измерений должен быть указан", nameof(approvedMeasuringInstrumentTypeId));
        }

        if (verificationIntervalYears < 1)
        {
            throw new ArgumentException("Межповерочный интервал должен быть положительным числом", nameof(verificationIntervalYears));
        }

        Id = Guid.NewGuid();
        SerialNumber = serialNumber.Trim();

        ApprovedMeasuringInstrumentTypeId = approvedMeasuringInstrumentTypeId;

        ModificationName = (string.IsNullOrWhiteSpace(modificationName)) 
            ? null 
            : modificationName.Trim();

        Composition = (string.IsNullOrWhiteSpace(composition))
            ? null
            : composition.Trim();

        VerificationIntervalYears = verificationIntervalYears;
    }
}

using System;

namespace PkrAssistant.Domain.Instruments;

/// <summary>
/// Конкретное средство измерений.
/// </summary>
public class MeasuringInstrument
{
    public Guid Id { get; private set; }
    public string SerialNumber { get; private set; }

    /// <summary>
    /// Межповерочный интервал в годах (может варьироваться между установленных внутри методики повеки значений).
    /// </summary>
    public int? VerificationIntervalYears { get; private set; }

    // Внешний ключ
    public Guid MeasuringInstrumentModificationId { get; private set; }

    // Навигационное свойство
    public MeasuringInstrumentModification Modification { get; private set; }

    // Для EF
    private MeasuringInstrument() {}

    public MeasuringInstrument(
        string serialNumber, 
        Guid measuringInstrumentModificationId,
        int? verificationIntervalYears = null)
    {
        if (string.IsNullOrWhiteSpace(serialNumber))
        {
            throw new ArgumentException("Заводской номер не может быть пустым", nameof(serialNumber));
        }

        if (measuringInstrumentModificationId == Guid.Empty)
        {
            throw new ArgumentException("Модификация должна быть указана", nameof(measuringInstrumentModificationId));
        }

        Id = Guid.NewGuid();
        SerialNumber = serialNumber.Trim();
        
        VerificationIntervalYears = verificationIntervalYears;
        MeasuringInstrumentModificationId = measuringInstrumentModificationId;
    }
}

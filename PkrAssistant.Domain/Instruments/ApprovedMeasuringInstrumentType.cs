using System;

namespace PkrAssistant.Domain.Instruments;

/// <summary>
/// Утверждённый тип средства измерений.
/// </summary>
public class ApprovedMeasuringInstrumentType
{
    public Guid Id { get; private set; }

    /// <summary>
    /// Уникальный регистрационный номер, присвоенный при утверждении типа средства измерений.
    /// </summary>
    public string RegistrationNumber { get; private set; }

    /// <summary>
    /// Тип средства измерений.
    /// </summary>
    public string TypeName { get; private set; }

    /// <summary>
    /// Полное наименование средства измерений.
    /// </summary>
    public string FullName { get; private set; }

    /// <summary>
    /// Межповерочный интервал в годах (может отличаться для конкретного экземпляра средства измерений).
    /// </summary>
    public int? VerificationIntervalYears { get; private set; }

    /// <summary>
    /// Идентификатор методики поверки типа средства измерений.
    /// </summary>
    public Guid VerificationMethodId { get; private set; }

    // Для EF
    private ApprovedMeasuringInstrumentType() {}

    public ApprovedMeasuringInstrumentType(
        string registrationNumber, 
        string typeName,
        string fullName,
        Guid verificationMethodId,
        int? verificationIntervalYears = null)
    {
        if (string.IsNullOrWhiteSpace(registrationNumber) == true)
        {
            throw new ArgumentException("Регистрационный номер не может быть пустым", nameof(registrationNumber));
        }

        if (string.IsNullOrWhiteSpace(typeName) == true)
        {
            throw new ArgumentException("Тип средства измерений не может быть пустым", nameof(typeName));
        }

        if (string.IsNullOrWhiteSpace(fullName) == true)
        {
            throw new ArgumentException("Наименование типа средства измерений не может быть пустым", nameof(fullName));
        }

        if (verificationMethodId == Guid.Empty)
        {
            throw new ArgumentException("Методика поверки должна быть указана", nameof(verificationMethodId)); 
        }

        Id = Guid.NewGuid();
        RegistrationNumber = registrationNumber.Trim();

        TypeName = typeName.Trim();
        FullName = fullName.Trim();

        VerificationIntervalYears = verificationIntervalYears;
        VerificationMethodId = verificationMethodId;
    }
}

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
    /// Шифр и наименование методики поверки типа средства измерений.
    /// </summary>
    public string VerificationMethodName { get; private set; }

    /// <summary>
    /// Дата начала срока действия свидетельства об утверждении типа средства измерений.
    /// </summary>
    public DateOnly ApprovalValidFrom { get; private set; }

    /// <summary>
    /// Дата окончания срока действия свидетельства об утверждении типа средства измерений.
    /// </summary>
    public DateOnly ApprovalExpiryDate { get; private set; }

    // Для EF
    private ApprovedMeasuringInstrumentType() {}

    public ApprovedMeasuringInstrumentType(
        string registrationNumber, 
        string typeName,
        string fullName,
        string verificationMethodName,
        DateOnly approvalValidFrom, 
        DateOnly approvalExpiryDate,
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

        if (string.IsNullOrWhiteSpace(verificationMethodName) == true)
        {
            throw new ArgumentException("Наименование методики поверки не может быть пустым", nameof(verificationMethodName)); 
        }

        if (approvalValidFrom > approvalExpiryDate)
        {
            throw new ArgumentException("Дата начала не может быть позднее даты окончания срока действия свидетельства об утверждении типа", nameof(approvalValidFrom));
        }

        Id = Guid.NewGuid();
        RegistrationNumber = registrationNumber.Trim();

        TypeName = typeName.Trim();
        FullName = fullName.Trim();

        VerificationIntervalYears = verificationIntervalYears;
        VerificationMethodName = verificationMethodName.Trim();

        ApprovalValidFrom = approvalValidFrom;
        ApprovalExpiryDate = approvalExpiryDate;
    }
}

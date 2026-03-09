using System;
using System.Collections.Generic;

namespace PkrAssistant.Domain.Standards;

/// <summary>
/// Эталон, применяемый при поверке.
/// </summary>
public class ReferenceStandard
{
    public Guid Id { get; private set; }

    /// <summary>
    /// Уникальный регистрационный номер, присвоенный при первичной аттестации эталона.
    /// </summary>
    public string RegistrationNumber { get; private set; }

    /// <summary>
    /// Полное наименование эталона.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Ключевые слова, по которым можно идентифицировать эталон.
    /// </summary>
    public string Keywords { get; private set; }

    /// <summary>
    /// Номер свидетельства об аттестации эталона.
    /// </summary>
    public string AttestationCertificateNumber { get; private set; }

    /// <summary>
    /// Срок действия свидетельства об аттестации эталона.
    /// </summary>
    public DateOnly AttestationExpiryDate { get; private set; }

    /// <summary>
    /// Находится ли эталон в эксплуатации.
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// Идентификаторы единиц измерений, которые хранит и передаёт эталон.
    /// </summary>
    public ICollection<Guid> UnitOfMeasurementIds { get; private set; }

    // Для EF
    private ReferenceStandard() { }

    public ReferenceStandard(
        string registrationNumber, 
        string name, 
        string keywords, 
        string attestationCertificateNumber, 
        DateOnly attestationExpiryDate,
        bool isActive)
    {
        if (string.IsNullOrWhiteSpace(registrationNumber) == true)
        {
            throw new ArgumentException("Регистрационный номер не может быть пустым", nameof(registrationNumber));
        }

        if (string.IsNullOrWhiteSpace(name) == true)
        {
            throw new ArgumentException("Наименование эталона не может быть пустым", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(keywords) == true)
        {
            throw new ArgumentException("Ключевое слова эталона не могут быть пустыми", nameof(keywords));
        }

        if (string.IsNullOrWhiteSpace(attestationCertificateNumber) == true)
        {
            throw new ArgumentException("Номер свидетельства об аттестации эталона не может быть пустым", nameof(attestationCertificateNumber));
        }

        if (attestationExpiryDate.Year < 2000)
        {
            throw new ArgumentException("Срок действия свидетельства об аттестации эталона должен быть не ранее 2000 года", nameof(attestationExpiryDate));
        }

        Id = Guid.NewGuid();

        RegistrationNumber = registrationNumber.Trim();
        Name = name.Trim();

        Keywords = keywords.Trim();
        AttestationCertificateNumber = attestationCertificateNumber.Trim();

        AttestationExpiryDate = attestationExpiryDate;
        IsActive = isActive;

        UnitOfMeasurementIds = new List<Guid>();
    }

    /// <summary>
    /// Возвращает true, если аттестация эталона действует на дату.
    /// </summary>
    /// <param name="date">Если null, подставляется текущая дата.</param>
    public bool IsAttestationValid(DateOnly? date = null)
    {
        if (date == null)
        {
            date = DateOnly.FromDateTime(DateTime.Today);
        }

        return date <= AttestationExpiryDate;
    }

    /// <summary>
    /// Добавляет идентификатор единицы измерений в список поддерживаемых эталоном.
    /// </summary>
    public void AddUnitOfMeasurement(Guid unitOfMeasurementId)
    {
        if (UnitOfMeasurementIds.Contains(unitOfMeasurementId) == true)
        {
            return;
        }

        UnitOfMeasurementIds.Add(unitOfMeasurementId);
    }

    /// <summary>
    /// Удаляет идентификатор единицы измерений из списка поддерживаемых эталоном.
    /// </summary>
    public void RemoveUnitOfMeasurement(Guid unitOfMeasurementId)
    {
        UnitOfMeasurementIds.Remove(unitOfMeasurementId);
    }
}

using System;

namespace PkrAssistant.Domain.Verification;

/// <summary>
/// Методика поверки на средство измерений
/// </summary>
public class VerificationMethod
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string CodeName { get; private set; }

    // Внешний ключ
    public Guid ApprovedMeasuringInstrumentTypeId { get; private set; }

    // Для EF
    private VerificationMethod() { }

    public VerificationMethod(
        string name, 
        string codename, 
        Guid approvedMeasuringInstrumentTypeId)
    {

        if (string.IsNullOrWhiteSpace(name) == true)
        {
            throw new ArgumentException("Наименование МП не может быть пустым", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(codename) == true)
        {
            throw new ArgumentException("Шифр МП не может быть пустым", nameof(codename));
        }

        if (approvedMeasuringInstrumentTypeId == Guid.Empty)
        {
            throw new ArgumentException("Идентификатор типа СИ должен быть указан", nameof(approvedMeasuringInstrumentTypeId));
        }

        Id = Guid.NewGuid();  
        Name = name.Trim();

        CodeName = codename.Trim();
        ApprovedMeasuringInstrumentTypeId = approvedMeasuringInstrumentTypeId;
    }
}

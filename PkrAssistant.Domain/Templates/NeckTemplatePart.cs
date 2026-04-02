using System;

namespace PkrAssistant.Domain.Templates;

/// <summary>
/// Часть шаблона, содержащая сведения о применяемых эталонах.
/// </summary>
public class NeckTemplatePart : TemplatePart
{
    /// <summary>
    /// Идентификатор средства измерений.
    /// </summary>
    public Guid MeasuringInstrumentId { get; private set; }

    /// <summary>
    /// Идентификатор "шапки" шаблона протокола.
    /// </summary>
    public Guid HeaderTemplatePartId { get; private set; }

    // Для EF
    private NeckTemplatePart() {}

    public NeckTemplatePart(
        Guid measuringInstrumentId,
        Guid headerTemplatePartId,
        string fileName,
        byte[] fileContent)
        : base(TemplatePartType.ReferenceStandards, fileName, fileContent)
    {
        if (measuringInstrumentId == Guid.Empty)
        {
            throw new ArgumentException("Идентификатор средства измерений должен быть заполнен", nameof(measuringInstrumentId));
        }

        if (headerTemplatePartId == Guid.Empty)
        {
            throw new ArgumentException("Идентификатор шапки шаблона должен быть заполнен", nameof(headerTemplatePartId));
        }

        Id = Guid.NewGuid();

        MeasuringInstrumentId = measuringInstrumentId;
        HeaderTemplatePartId = headerTemplatePartId;
    }

    // Для отладки
    public override string ToString()
    {
        return $"{FileName} (Instrument = {MeasuringInstrumentId}, Header = {HeaderTemplatePartId})";
    }
}
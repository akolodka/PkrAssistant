using System;

namespace PkrAssistant.Domain.Templates;

/// <summary>
/// Часть шаблона поверки, содержащая блок определения конкретной метрологической характеристики.
/// </summary>
public class MetrologicalInspectionPart : TemplatePart
{
    /// <summary>
    /// Идентификатор подразделения шаблона (для гарантии сохранности ширины шаблона).
    /// </summary>
    public Guid DepartmentId { get; private set; }

    // Для EF
    private MetrologicalInspectionPart() {}

    public MetrologicalInspectionPart(
        Guid departmentId, 
        string fileName, 
        byte[] fileContent)
        : base(TemplatePartType.MetrologicalInspection, fileName, fileContent)
    {
        if (departmentId == Guid.Empty)
        {
            throw new ArgumentException("Идентификатор подразделения должен быть указан", nameof(departmentId));
        }

        DepartmentId = departmentId;
    }

    // Для отладки
    public override string ToString()
    {
        return $"{FileName} (Department = {DepartmentId}, Type = {TemplatePartType.MetrologicalInspection})";
    }
}
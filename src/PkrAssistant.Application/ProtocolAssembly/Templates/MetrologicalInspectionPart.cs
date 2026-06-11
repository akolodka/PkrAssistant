using System;

namespace PkrAssistant.Application.Templates;

/// <summary>
/// Часть шаблона поверки, содержащая блок определения конкретной метрологической характеристики.
/// </summary>
public class MetrologicalInspectionPart : TemplatePart
{
    // Для EF
    private MetrologicalInspectionPart() {}

    public MetrologicalInspectionPart(
        Guid departmentId, 
        string fileName, 
        byte[] fileContent)
        : base(TemplatePartType.MetrologicalInspection, fileName, fileContent, departmentId)
    {}

    // Для отладки
    public override string ToString()
    {
        return $"{FileName} (Department = {DepartmentId}, Type = {TemplatePartType.MetrologicalInspection})";
    }
}
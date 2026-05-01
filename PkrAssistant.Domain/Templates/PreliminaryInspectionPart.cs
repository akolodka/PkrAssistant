using System;

namespace PkrAssistant.Domain.Templates;

/// <summary>
/// Часть шаблона поверки, содержащая блок внешнего осмотра и опробования.
/// </summary>
public class PreliminaryInspectionPart : TemplatePart
{
    // Для EF
    private PreliminaryInspectionPart() {}

    public PreliminaryInspectionPart(
        Guid departmentId, 
        string fileName,
        byte[] fileContent)
        : base(TemplatePartType.PreliminaryInspection, fileName, fileContent, departmentId)
    {}

    // Для отладки
    public override string ToString()
    {
        return $"{FileName} (Department = {DepartmentId}, Type = {TemplatePartType.PreliminaryInspection})";
    }
}
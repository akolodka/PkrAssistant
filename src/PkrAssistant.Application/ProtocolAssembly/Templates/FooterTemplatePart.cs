using System;

namespace PkrAssistant.Application.Templates;

/// <summary>
/// Часть шаблона поверки, содержащая заключение по результатам поверки и подпись.
/// </summary>
public class FooterTemplatePart : TemplatePart
{
    // Для EF
    private FooterTemplatePart() {}

    public FooterTemplatePart(
        Guid departmentId,
        string fileName,
        byte[] fileContent)
        : base(TemplatePartType.Footer, fileName, fileContent, departmentId)
    {}

    // Для отладки
    public override string ToString()
    {
        return $"{FileName} (Department = {DepartmentId}, Type = {TemplatePartType.Footer})";
    }
}
using System;

namespace PkrAssistant.Domain.Templates;

/// <summary>
/// Заголовочная часть шаблона поверки.
/// </summary>
public class HeaderTemplatePart : TemplatePart
{
    // Для EF
    private HeaderTemplatePart() {}

    public HeaderTemplatePart(
        Guid departmentId,
        string fileName,
        byte[] fileContent)
        : base(TemplatePartType.Header, fileName, fileContent, departmentId)
    {}

    // Для отладки
    public override string ToString()
    {
        return $"{FileName} (Department = {DepartmentId}, Type = {TemplatePartType.Header})";
    }
}
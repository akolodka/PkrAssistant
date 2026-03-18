using System;

namespace PkrAssistant.Domain.Templates;

/// <summary>
/// Заголовочная часть шаблона поверки.
/// </summary>
public class HeaderTemplatePart : TemplatePart
{
    /// <summary>
    /// Идентификатор подразделения, применяющего шаблон.
    /// </summary>
    public Guid DepartmentId { get; private set; }

    // Для EF
    private HeaderTemplatePart() {}

    public HeaderTemplatePart(
        string fileName,
        byte[] fileContent,
        Guid departmentId)
        : base(TemplatePartType.Header, fileName, fileContent)
    {
        if (departmentId == Guid.Empty)
        {
            throw new ArgumentException("Идентификатор подразделения должен быть заполнен", nameof(departmentId));
        }

        DepartmentId = departmentId;
    }

    // Для отладки
    public override string ToString()
    {
        return $"{FileName} ({DepartmentId})";
    }
}
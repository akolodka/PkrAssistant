using System;

namespace PkrAssistant.Domain.Templates;

/// <summary>
/// Часть шаблона поверки, содержащая заключение по результатам поверки и подпись.
/// </summary>
public class FooterTemplatePart : TemplatePart
{
    /// <summary>
    /// Идентификатор подразделения шаблона (для гарантии сохранности ширины шаблона).
    /// </summary>
    public Guid DepartmentId { get; private set; }

    // Для EF
    private FooterTemplatePart() {}

    public FooterTemplatePart(
        Guid departmentId,
        string fileName,
        byte[] fileContent)
        : base(TemplatePartType.Footer, fileName, fileContent)
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
        return $"{FileName} (Department = {DepartmentId}, Type = {TemplatePartType.Footer})";
    }
}
using System;

namespace PkrAssistant.Application.Organization;

/// <summary>
/// Подразделение организации, выполняющее поверку.
/// </summary>
public class Department
{
    public Guid Id { get; private set; }

    /// <summary>
    /// Наименование подразделения (не более 50 символов).
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Комментарий к записи (не более 100 символов).
    /// </summary>
    public string Comment { get; private set; }

    // Для EF
    private Department() {}

    public Department(string name, string comment)
    {
        if (string.IsNullOrWhiteSpace(name) == true)
        {
            throw new ArgumentException("Наименование подразделения не может быть пустым", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(comment) == true)
        {
            throw new ArgumentException("Комментарий не может быть пустым", nameof(comment));
        }

        Id = Guid.NewGuid();

        Name = name.Trim();
        Comment = comment.Trim();
    }

    public override string ToString()
    {
        return Name;
    }
}
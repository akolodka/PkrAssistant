using System;

namespace PkrAssistant.Domain.Verification;

/// <summary>
/// Результат поверки.
/// </summary>
public class VerificationResult
{
    public Guid Id { get; private set; }

    /// <summary>
    /// Краткое наименование результата поверки.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Полное описание результата поверки - будет передано в документ.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Флаг пригодности к применению.
    /// </summary>
    public bool IsPass { get; private set; }

    /// <summary>
    /// Порядок отображения результата в списке (чем меньше число, тем выше позиция).
    /// </summary>
    public int SortOrder { get; private set; }

    // Для EF
    private VerificationResult() {}

    public VerificationResult(
        string name, 
        string description, 
        bool isPass, 
        int sortOrder)
    {
        if (string.IsNullOrWhiteSpace(name) == true)
        {
            throw new ArgumentException("Краткое наименование результата поверки не может быть пустым", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(description) == true)
        {
            throw new ArgumentException("Полное описание результата поверки не может быть пустым", nameof(description));
        }

        if (sortOrder < 0)
        {
            throw new ArgumentException("Порядок сортировки должен быть положительным числом", nameof(sortOrder));
        }

        Id = Guid.NewGuid();

        Name = name.Trim();
        Description = description.Trim();

        IsPass = isPass;
        SortOrder = sortOrder;
    }
}

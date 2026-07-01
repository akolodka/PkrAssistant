using System;

namespace PkrAssistant.Application.Standards;

/// <summary>
/// Разряд эталона.
/// </summary>
public class StandardRank
{
    public Guid Id { get; private set; }

    /// <summary>
    /// Наименование разряда эталона.
    /// </summary>
    public string Name { get; private set; }

    // Для EF
    private StandardRank() {}

    public StandardRank(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Наименование разряда эталона не может быть пустым", nameof(name));
        }

        Id = Guid.NewGuid();
        Name = name.Trim();
    }

    public override string ToString()
    {
        return Name;
    }
}

using System;

namespace PkrAssistant.Domain.Protocols;

/// <summary>
/// Форма протокола поверки для заполнения поверителем.
/// </summary>
public class ProtocolForm 
{
    public Guid Id { get; private set; }

    /// <summary>
    /// Наименование формы протокола поверки.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Идентификатор файла шаблона поверки.
    /// </summary>
    public Guid TemplateFileId { get; private set; }

    /// <summary>
    /// Флаг состояния доступности формы протокола для работы.
    /// </summary>
    public bool IsActive { get; private set; }

    /// <summary>
    /// Дата создания формы протокола поверки.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Дата изменения формы протокола поверки.
    /// </summary>
    public DateTime UpdatedAt { get; private set; }

    // Для EF
    private ProtocolForm() {}

    public ProtocolForm(
        string name, 
        Guid templateFileId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Наименование формы протокола поверки не может быть пустым", nameof(name));
        }

        if (templateFileId == Guid.Empty)
        {
            throw new ArgumentException("Идентификатор файла протокола поверки должен быть указан", nameof(templateFileId));
        }

        Id = Guid.NewGuid();
        Name = name;

        TemplateFileId = templateFileId;
        IsActive = true;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Переводит форму протокола поверки в архивное состояние.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Переводит форму протокола поверки в действительное состояние.
    /// </summary>
    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
}

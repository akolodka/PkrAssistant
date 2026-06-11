using System;

namespace PkrAssistant.Application.Templates;

/// <summary>
/// Базовый класс для составных частей шаблона поверки.
/// </summary>
public abstract class TemplatePart
{
    public Guid Id { get; protected set; }

    /// <summary>
    /// Имя файла шаблона для отображения в интерфейсе (без пути).
    /// </summary>
    public string FileName { get; protected set; }

    /// <summary>
    /// Байтовый массив содержимого файла шаблона (для хранения в БД).
    /// </summary>
    public byte[] FileContent { get; protected internal set; }

    /// <summary>
    /// Тип части шаблона (используется как дискриминатор в БД).
    /// </summary>
    public TemplatePartType Type { get; private set; }

    /// <summary>
    /// Идентификатор подразделения шаблона (для гарантии сохранности ширины шаблона).
    /// </summary>
    public Guid DepartmentId { get; private set; }

    // Для EF
    protected TemplatePart() {}

    protected TemplatePart(
        TemplatePartType type, 
        string fileName, 
        byte[] fileContent, 
        Guid departmentId)
    {
        if (string.IsNullOrWhiteSpace(fileName) == true)
        {
            throw new ArgumentException("Имя файла не может быть пустым", nameof(fileName));
        }

        if (fileContent == null)
        {
           throw new ArgumentNullException(nameof(fileContent), "Файл должен быть загружен");
        }

        if (fileContent.Length == 0)
        {
            throw new ArgumentException("Содержимое файла шаблона не может быть пустым", nameof(fileContent));
        }

        if (departmentId == Guid.Empty)
        {
            throw new ArgumentException("Идентификатор подразделения должен быть заполнен", nameof(departmentId));
        }

        Id = Guid.NewGuid();
        Type = type;

        FileName = fileName.Trim();
        FileContent = fileContent;

        DepartmentId = departmentId;
    }

    // Для отладки
    public override string ToString()
    {
        return $"{Type}: {FileName}";
    }
}
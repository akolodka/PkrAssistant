using System;

namespace PkrAssistant.Domain.Templates;

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
    public byte[] FileContent { get; protected set; }

    /// <summary>
    /// Тип части шаблона (используется как дискриминатор в БД).
    /// </summary>
    public TemplatePartType Type { get; private set; }

    // Для EF
    protected TemplatePart() {}

    protected TemplatePart(
        TemplatePartType type, 
        string fileName, 
        byte[] fileContent)
    {
        if (string.IsNullOrWhiteSpace(fileName) == true)
        {
            throw new ArgumentException("Имя файла не может быть пустым", nameof(fileName));
        }

        if (fileContent == null)
        {
           throw new ArgumentNullException(nameof(fileContent)); 
        }

        Id = Guid.NewGuid();
        Type = type;

        FileName = fileName.Trim(); 
        FileContent = fileContent;

    }
}

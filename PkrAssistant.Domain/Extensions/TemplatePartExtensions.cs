using PkrAssistant.Domain.Templates;
using System;
using System.Collections.Generic;
using System.IO;

namespace PkrAssistant.Domain.Extensions;

/// <summary>
/// Методы расширений для составных частей шаблона поверки.
/// </summary>
public static class TemplatePartExtensions
{
    private static readonly HashSet<string> AllowedExtensions = new (
        new[] { ".xlsx", ".docx" },
        StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Возвращает true, если расширение файла-шаблона разрешено.
    /// </summary>
    public static bool HasValidExtension(this TemplatePart part)
    {

        if (string.IsNullOrWhiteSpace(part.FileName) == true)
        {
            return false;
        }

        var extension = Path.GetExtension(part.FileName);

        return AllowedExtensions.Contains(extension);
    }
}

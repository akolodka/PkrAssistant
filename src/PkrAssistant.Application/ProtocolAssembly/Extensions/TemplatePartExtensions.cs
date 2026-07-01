using PkrAssistant.Application.Templates;
using System;
using System.Collections.Generic;
using System.IO;

namespace PkrAssistant.Application.Extensions;

/// <summary>
/// Методы расширений для составных частей шаблона поверки.
/// </summary>
public static class TemplatePartExtensions
{
    private static readonly HashSet<string> _allowedExtensions = new (
        new[] { ".xlsx", ".docx" },
        StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Возвращает true, если расширение файла-шаблона разрешено.
    /// </summary>
    public static bool HasValidExtension(this TemplatePart part)
    {
        if (string.IsNullOrWhiteSpace(part.FileName))
        {
            return false;
        }

        var extension = Path.GetExtension(part.FileName);

        var hasValidExtension = _allowedExtensions.Contains(extension);

        return hasValidExtension;
    }
}

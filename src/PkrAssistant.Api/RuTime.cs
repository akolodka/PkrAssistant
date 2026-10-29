using System;
using System.Globalization;

namespace PkrAssistant.Api;

/// <summary>
/// Форматирует передаваемую дату согласно ru-RU незавивисимо от внешней культуры
/// </summary>
public readonly struct RuTime : IFormattable
{
    private readonly DateTime _time;

    public RuTime(DateTime time)
    {
        _time = time;
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return _time.ToString(format, CultureInfo.GetCultureInfo("ru-RU"));
    }

    // Возвращает дату в формате dd.MM.yyyy HH:mm:ss по умолчанию
    public override string ToString()
    {
        return ToString("G", null);
    }
}
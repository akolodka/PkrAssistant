using System;

namespace PkrAssistant.Api.Extensions;

public static class DateTimeExtensions
{
    public static RuTime AsRuTime(this DateTime time)
    {
        return new RuTime(time);
    }
}

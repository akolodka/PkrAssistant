namespace PkrAssistant.Application.Templates;

/// <summary>
/// Типы составных частей шаблона для поверки.
/// </summary>
public enum TemplatePartType
{
    /// <summary>
    /// Заголовок шаблона поверки.
    /// </summary>
    Header = 1,

    /// <summary>
    /// Блок сведений об эталонах.
    /// </summary>
    ReferenceStandards = 2,

    /// <summary>
    /// Блок предварительных мероприятий поверки (внешний осмотр, опробование).
    /// </summary>
    PreliminaryInspection = 3,

    /// <summary>
    /// Основной блок мероприятий поверки (определение метрологических характеристик).
    /// </summary>
    MetrologicalInspection = 4,

    /// <summary>
    /// Заключение и подписи.
    /// </summary>
    Footer = 5
}
namespace PkrAssistant.Domain.Templates;

/// <summary>
/// Типы составных частей шаблона для поверки.
/// </summary>
public enum TemplatePartType
{
    /// <summary>
    /// Заголовок шаблона поверки.
    /// </summary>
    Header,

    /// <summary>
    /// Блок сведений об эталонах.
    /// </summary>
    ReferenceStandards,

    /// <summary>
    /// Блок предварительных мероприятий поверки (внешний осмотр, опробование).
    /// </summary>
    PreliminaryInspection,

    /// <summary>
    /// Основной блок мероприятий поверки (определение метрологических характеристик).
    /// </summary>
    MetrologicalInspection,

    /// <summary>
    /// Заключение и подписи.
    /// </summary>
    Footer
}

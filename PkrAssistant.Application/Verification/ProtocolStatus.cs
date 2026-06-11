namespace PkrAssistant.Domain.Verification;

/// <summary>
/// Статус протокола поверки.
/// </summary>
public enum ProtocolStatus
{
    /// <summary>
    /// Черновик (можно редактировать).
    /// </summary>
    Draft = 0,

    /// <summary>
    /// Завершён (нельзя редактировать).
    /// </summary>
    Final = 1,

    /// <summary>
    /// Отменён (нельзя редактировать).
    /// </summary>
    Cancelled = 2
}

namespace PkrAssistant.Application.ProtocolAssembly.Validation;

/// <summary>
/// Определяет контракт для проверки бизнес-правил набора частей шаблона поверки.
/// </summary>
public interface IProtocolAssemblyValidator
{
    /// <summary>
    /// Проверяет корректность сборки шаблона.
    /// </summary>
    /// <param name="failureAssemblyResult">Отрицательный результат валидации.</param>
    /// <returns></returns>
    bool TryValidate(out ProtocolAssemblyResult? failureAssemblyResult);
}
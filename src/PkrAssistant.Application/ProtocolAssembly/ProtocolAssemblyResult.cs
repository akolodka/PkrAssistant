namespace PkrAssistant.Application.ProtocolAssembly;

/// <summary>
/// Результат сборки шаблона поверки. Возвращается сервисом вместо исключений для ожидаемых сценариев.
/// </summary>
public record ProtocolAssemblyResult
{
    /// <summary>
    /// Признак успешной сборки шаблона поверки.
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    /// Содержимое шаблона поверки. Присутствует, если IsSuccess = true.
    /// </summary>
    public byte[]? FileContent { get; init; }

    /// <summary>
    /// Текст критической ошибки, при которой невозможна сборка шаблона.
    /// </summary>
    public string? ErrorMessage { get; init; }

    /// <summary>
    /// Приватный конструктор. Создание объекта только через фабрики.
    /// </summary>
    private ProtocolAssemblyResult(
        bool isSuccess,
        byte[]? fileContent,
        string? errorMessage)
    {
        IsSuccess = isSuccess;
        FileContent = fileContent;

        ErrorMessage = errorMessage;
    }

    public static ProtocolAssemblyResult Success(byte[] fileContent)
    {
        return new ProtocolAssemblyResult(
            isSuccess: true,
            fileContent: fileContent,
            errorMessage: null);
    }

    public static ProtocolAssemblyResult Failure(string errorMessage)
    {
        return new ProtocolAssemblyResult(
            isSuccess: false,
            fileContent: null,
            errorMessage: errorMessage);
    }
}
using System.Linq;

namespace PkrAssistant.Application.ProtocolAssembly.Validation;

/// <summary>
/// Валидирует запрос на сборку шаблона.
/// </summary>
public class ProtocolAssemblyRequestValidator : IProtocolAssemblyValidator
{
    private readonly AssemblyRequest? _request;

    public ProtocolAssemblyRequestValidator(AssemblyRequest? request)
    {
        _request = request;
    }

    public bool TryValidate(out ProtocolAssemblyResult? failureAssemblyResult)
    {
        if (_request?.TemplatePartIds is null || _request.TemplatePartIds.Any() is false)
        {
            failureAssemblyResult = ProtocolAssemblyResult.Failure("Запрос не содержит идентификаторы частей шаблона.");
            return false;
        }

        failureAssemblyResult = null;
        return true;
    }
}

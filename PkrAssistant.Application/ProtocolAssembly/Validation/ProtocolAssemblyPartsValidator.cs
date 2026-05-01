using PkrAssistant.Domain.Templates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PkrAssistant.Application.ProtocolAssembly.Validation;

/// <summary>
/// Валидирует части шаблона перед сборкой.
/// </summary>
public class ProtocolAssemblyPartsValidator : IProtocolAssemblyValidator
{
    private readonly IReadOnlyList<TemplatePart> _parts;
    private readonly IReadOnlyList<Guid> _requestIds;

    public ProtocolAssemblyPartsValidator(
        IReadOnlyList<TemplatePart> parts,
        IReadOnlyList<Guid> requestIds)
    {
        _parts = parts;
        _requestIds = requestIds;
    }

    public bool TryValidate(out ProtocolAssemblyResult? failureAssemblyResult)
    {
        if (_parts.Count < _requestIds.Count)
        {
            var missingIds = _requestIds.Except(_parts.Select(p => p.Id));

            failureAssemblyResult = ProtocolAssemblyResult.Failure($"Не найдены части: {string.Join(", ", missingIds)}");
            return false;
        }

        if (_parts.Any(p => p.FileContent == null || p.FileContent.Length == 0))
        {
            failureAssemblyResult = ProtocolAssemblyResult.Failure("Одна из частей шаблона не содержит данные.");
            return false;
        }

        failureAssemblyResult =  null;
        return true;
    }
}
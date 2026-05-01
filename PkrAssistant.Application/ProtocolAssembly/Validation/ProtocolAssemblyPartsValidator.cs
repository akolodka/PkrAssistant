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
        // Проверка на количество частей
        if (_parts.Count < _requestIds.Count)
        {
            var missingIds = _requestIds.Except(_parts.Select(p => p.Id));

            failureAssemblyResult = ProtocolAssemblyResult.Failure($"Не найдены части: {string.Join(", ", missingIds)}");
            return false;
        }

        // Проверка на наличие даных в частях шаблонов
        if (_parts.Any(p => p.FileContent == null || p.FileContent.Length == 0))
        {
            failureAssemblyResult = ProtocolAssemblyResult.Failure("Одна из частей шаблона не содержит данные.");
            return false;
        }

        // Проверка на то, что все части принадлежат одному подразделению
        var departmentsCount = _parts
            .Select(p => p.DepartmentId)
            .Distinct()
            .Count();

        if (departmentsCount > 1)
        {
            failureAssemblyResult = ProtocolAssemblyResult.Failure("Части шаблона принадлежат разным отделам.");
            return false;
        }

        // Проверка на то, что найдены все необходимые части
        var referenceTypes = Enum.GetValues<TemplatePartType>();

        var partTypes = _parts
            .Select(p => p.Type)
            .Distinct();

        var missingTypes = referenceTypes.Except(partTypes);

        if (missingTypes.Any() == true)
        {
            failureAssemblyResult = ProtocolAssemblyResult.Failure($"Недостаёт частей шаблона: {string.Join(", ", missingTypes)}.");
            return false;
        }

        failureAssemblyResult =  null;
        return true;
    }
}
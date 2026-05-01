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
        if (TryValidatePartsCount(out failureAssemblyResult) == false)
        {
            return false;
        }

        if (TryValidateFileContent(out failureAssemblyResult) == false)
        {
            return false;
        }

        if (TryValidateDistinctDepartment(out failureAssemblyResult) == false)
        {
            return false;
        }
        
        if (TryValidateMissingProtocolTypes(out failureAssemblyResult) == false)
        {
            return false;
        }

        if (TryValidateDuplicatePartTypes(out failureAssemblyResult) == false)
        {
            return false;
        }

        failureAssemblyResult = null;
        return true;
    }

    /// <summary>
    /// Проверяет количество возвращаемых частей шаблона.
    /// </summary>
    /// <param name="failureAssemblyResult">Отрицательный результат сборки шаблона.</param>
    /// <returns>True, если количество возвращаемых частей шаблона совпадает с количеством запрошенных частей шаблона.</returns>
    private bool TryValidatePartsCount(out ProtocolAssemblyResult? failureAssemblyResult)
    {
        if (_parts.Count < _requestIds.Count)
        {
            var missingIds = _requestIds.Except(_parts.Select(p => p.Id));

            failureAssemblyResult = ProtocolAssemblyResult.Failure($"Не найдены части: {string.Join(", ", missingIds)}");
            return false;
        }

        failureAssemblyResult = null;
        return true;
    }

    /// <summary>
    /// Проверяет наполнение частей шаблона.
    /// </summary>
    /// <param name="failureAssemblyResult">Отрицательный результат сборки шаблона.</param>
    /// <returns>True, если все части шаблона содержат данные.</returns>
    private bool TryValidateFileContent(out ProtocolAssemblyResult? failureAssemblyResult)
    {
        var empties = _parts
            .Where(p => p.FileContent == null || p.FileContent.Length == 0)
            .Select(e => e.Id);

        if (empties.Any() == true)
        {
            failureAssemblyResult = ProtocolAssemblyResult.Failure($"Эти части шаблона не содержат данные: {string.Join(", ", empties)}.");
            return false;
        }

        failureAssemblyResult = null;
        return true;
    }

    /// <summary>
    /// Проверяет принадлежность к подразделению частей шаблона.
    /// </summary>
    /// <param name="failureAssemblyResult">Отрицательный результат сборки шаблона.</param>
    /// <returns>True, если все части шаблона принадлежат одному подразделению.</returns>
    private bool TryValidateDistinctDepartment(out ProtocolAssemblyResult? failureAssemblyResult)
    {
        var departmentsCount = _parts
            .Select(p => p.DepartmentId)
            .Distinct()
            .Count();

        if (departmentsCount > 1)
        {
            failureAssemblyResult = ProtocolAssemblyResult.Failure("Части шаблона принадлежат разным отделам.");
            return false;
        }

        failureAssemblyResult = null;
        return true;
    }

    /// <summary>
    /// Проверяет наличие недостающих частей шаблона.
    /// </summary>
    /// <param name="failureAssemblyResult">Отрицательный результат сборки шаблона.</param>
    /// <returns>True, если присутствуют все типы частей шаблона.</returns>
    private bool TryValidateMissingProtocolTypes(out ProtocolAssemblyResult? failureAssemblyResult)
    {
        var referenceTypes = Enum.GetValues<TemplatePartType>();

        var partTypes = _parts
            .Select(p => p.Type);

        var missingTypes = referenceTypes.Except(partTypes);

        if (missingTypes.Any() == true)
        {
            failureAssemblyResult = ProtocolAssemblyResult.Failure($"Недостаёт частей шаблона: {string.Join(", ", missingTypes)}.");
            return false;
        }

        failureAssemblyResult = null;
        return true;
    }

    /// <summary>
    /// Проверяет наличие дубликатов типов частей шаблона.
    /// </summary>
    /// <param name="failureAssemblyResult">Отрицательный результат сборки шаблона.</param>
    /// <returns>True, если все части шаблона представлены в единственном экземпляре.</returns>
    private bool TryValidateDuplicatePartTypes(out ProtocolAssemblyResult? failureAssemblyResult)
    {
        var duplicates = _parts
            .Select(p => p.Type)
            .GroupBy(p => p)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);

        if (duplicates.Any() == true)
        {
            failureAssemblyResult = ProtocolAssemblyResult.Failure($"Обнаружены дубликаты частей шаблонов: {string.Join(", ", duplicates)}.");
            return false;
        }

        failureAssemblyResult = null;
        return true;
    }
}
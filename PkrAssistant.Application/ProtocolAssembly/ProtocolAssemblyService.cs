using PkrAssistant.Domain.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PkrAssistant.Application.ProtocolAssembly;

/// <summary>
/// Собирает шаблон поверки.
/// </summary>
public class ProtocolAssemblyService : IProtocolAssemblyService
{
    private readonly ITemplatePartProvider _provider;
    private readonly IFileAssembler _assembler;

    public ProtocolAssemblyService(
        ITemplatePartProvider provider, 
        IFileAssembler assembler)
    {
        _provider = provider;
        _assembler = assembler;
    }

    /// <summary>
    /// Собирает шаблон поверки на основе запроса.
    /// </summary>
    /// <param name="request">Запрос, содержащий Id частей шаблона.</param>
    /// <returns>Результат сборки в виде ProtocolAssemblyResult.</returns>
    public async Task<ProtocolAssemblyResult> AssembleAsync(AssemblyRequest request)
    {
        var isRequestValid = TryValidateRequest(request, out var invalidAssemblyResult);

        if (isRequestValid == false)
        {
            return invalidAssemblyResult;
        }

        var parts = await _provider.GetPartsByIdsAsync(request.TemplatePartIds);

        var isPartsValid = TryValidateAssemblyParts(parts, request.TemplatePartIds, out var invalidAssemblyPartsResult);

        if (isPartsValid == false)
        {
            return invalidAssemblyPartsResult;
        } 

        // Сортировка по логическому порядку сборки (порядок определяется значениями enum TemplatePartType)
        var fileContent = await _assembler.AssembleAsync(
            parts.OrderBy(p => p.Type)
                .Select(p => p.FileContent)
                .ToArray());

        return ProtocolAssemblyResult.Success(fileContent);
    }

    private bool TryValidateRequest(AssemblyRequest request, out ProtocolAssemblyResult? assemblyResult)
    {
        if (request?.TemplatePartIds == null || request.TemplatePartIds.Any() == false)
        {
            assemblyResult = ProtocolAssemblyResult.Failure("Запрос не содержит идентификаторы частей шаблона.");
            return false; 
        }

        assemblyResult = null;
        return true;
    }

    private bool TryValidateAssemblyParts(
        IReadOnlyList<TemplatePart> parts,
        IReadOnlyList<Guid> requestIds, 
        out ProtocolAssemblyResult? assemblyResult)
    {
        if (parts.Count < requestIds.Count)
        {
            var missingIds = requestIds.Except(parts.Select(p => p.Id));

            assemblyResult = ProtocolAssemblyResult.Failure($"Не найдены части: {string.Join(", ", missingIds)}");
            return false;
        }

        if (parts.Any(p => p.FileContent == null || p.FileContent.Length == 0))
        {
            assemblyResult = ProtocolAssemblyResult.Failure("Одна из частей шаблона не содержит данные.");
            return false;
        }

        assemblyResult = null;
        return true;
    }
}
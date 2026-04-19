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
        if (request?.TemplatePartIds == null || request.TemplatePartIds.Any() == false)
        {
            return ProtocolAssemblyResult.Failure("Запрос не содержит идентификаторы частей шаблона.");
        }

        var parts = await _provider.GetPartsByIdsAsync(request.TemplatePartIds);

        if (parts.Count < request.TemplatePartIds.Count)
        {
            var missingIds = request.TemplatePartIds
                .Except(parts.Select(p => p.Id));

            return ProtocolAssemblyResult.Failure($"Не найдены части: {string.Join(", ", missingIds)}");
        }

        if (parts.Any(p => p.FileContent == null || p.FileContent.Length == 0))
        {
            return ProtocolAssemblyResult.Failure("Одна из частей шаблона не содержит данные.");
        }

        // Сортировка по логическому порядку сборки, порядок определяется значениями enum TemplatePartType
        var fileContent = await _assembler.AssembleAsync(
            parts.OrderBy(p => p.Type)
                .Select(p => p.FileContent)
                .ToArray());

        return ProtocolAssemblyResult.Success(fileContent);
    }
}
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

    public Task<ProtocolAssemblyResult> AssembleAsync(AssemblyRequest request)
    {
        return Task.FromResult(ProtocolAssemblyResult.Failure("Not implemented"));
    }
}
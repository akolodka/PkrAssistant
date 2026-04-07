using System.Threading.Tasks;

namespace PkrAssistant.Application.ProtocolAssembly;

/// <summary>
/// Контракт сборки документа-шаблона поверки.
/// </summary>
public interface IProtocolAssemblyService
{
    /// <summary>
    /// Собирает документ из частей.
    /// </summary>
    Task<ProtocolAssemblyResult> AssembleAsync(AssemblyRequest request);
}
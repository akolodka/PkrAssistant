using System.Collections.Generic;
using System.Threading.Tasks;

namespace PkrAssistant.Application.ProtocolAssembly;

/// <summary>
/// Контракт сборки шаблона из составных частей.
/// </summary>
public interface IFileAssembler
{
    /// <summary>
    /// Собирает шаблон поверки из составных частей.
    /// </summary>
    /// <param name="templatePartContents">Содержимое частей шаблонов поверки в порядке сборки.</param>
    /// <returns>Файл шаблона поверки.</returns>
    Task<byte[]> AssembleAsync(IEnumerable<byte[]> templatePartContents);
}
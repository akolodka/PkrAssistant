using PkrAssistant.Application.ProtocolAssembly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PkrAssistant.Application.Tests.Fakes;

/// <summary>
/// Заглушка для тестирования сборки частей шаблоа.
/// </summary>
public class FakeFileAssembler : IFileAssembler
{
    /// <summary>
    /// Склеивает части шаблона в один файл в памяти.
    /// </summary>
    /// <param name="templatePartContents">Список байтовых массивов частей шаблона в порядке склеивания.</param>
    /// <returns>Объединённый массив байтов шаблона.</returns>
    public Task<byte[]> AssembleAsync(IEnumerable<byte[]> templatePartContents)
    {
        if (templatePartContents == null)
        {
            throw new ArgumentNullException("Список байтовых массивов частей шаблона не может быть null", nameof(templatePartContents));
        }

        using var stream = new MemoryStream();

        foreach (byte[] content in templatePartContents)
        {
            if(content == null)
            {
                throw new ArgumentNullException("Содержимое части шаблона не может быть null", nameof(content));
            }

            stream.Write(content);
        }

        return Task.FromResult(stream.ToArray());
    }
}
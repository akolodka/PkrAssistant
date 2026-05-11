using ClosedXML.Excel;
using PkrAssistant.Application.ProtocolAssembly;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PkrAssistant.Infrastructure;

/// <summary>
/// «Боевой» сервис сборки частей шаблона. 
/// </summary>
public class ClosedXmlFileAssembler : IFileAssembler
{
    /// <summary>
    /// Склеивает части шаблона в один файл в памяти.
    /// </summary>
    /// <param name="templatePartContents">Список байтовых массивов частей шаблона в порядке склеивания.</param>
    /// <returns>Объединённый массив байтов шаблона.</returns>
    public async Task<byte[]> AssembleAsync(IEnumerable<byte[]> templatePartContents) 
    {
        using var stream = new MemoryStream();
        using var destination = new XLWorkbook();

        var worksheet = destination.AddWorksheet();

        foreach (var content in templatePartContents)
        {
            ProcessAssemble(worksheet, content);
        }

        destination.SaveAs(stream);

        var fileContent = stream.ToArray();

        return await Task.FromResult(fileContent);
    }

    /// <summary>
    /// Добавляет содержимое части шаблона в конец целевого листа.
    /// </summary>
    /// <param name="worksheet">Лист, который выполняется вставка данных.</param>
    /// <param name="fileContent">Содержимое, подлежащее вставке.</param>
    private void ProcessAssemble(IXLWorksheet worksheet, byte[] fileContent)
    {
        using var stream = new MemoryStream(fileContent);
        using var book = new XLWorkbook(stream);

        var source = book.Worksheet(1)
            .RangeUsed(XLCellsUsedOptions.AllContents);
        
        if (source == null)
        {
            return;
        }

        var lastUsedCell = worksheet.LastCellUsed(XLCellsUsedOptions.AllContents);

        var destination = (lastUsedCell == null)
            ? worksheet.Cell("A1")
            : lastUsedCell.WorksheetRow()
                .RowBelow()
                .FirstCell();

        source.CopyTo(destination);
    }
}
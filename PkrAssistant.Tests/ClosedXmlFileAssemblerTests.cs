using ClosedXML.Excel;
using PkrAssistant.Infrastructure;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PkrAssistant.Tests;

public class ClosedXmlFileAssemblerTests
{
    [Fact]
    public async Task AssembleAsync_ReturnsFileContent()
    {
        // Arrange
        var assembler = new ClosedXmlFileAssembler();

        // Act
        var fileContent = await assembler.AssembleAsync(Array.Empty<byte[]>());

        // Assert
        Assert.True(fileContent.Length > 0);
    }

    [Fact]
    public async Task AssembleAsync_WithStubContent_ReturnsCombinedContent()
    {
        // Arrange
        using var book = new XLWorkbook();
        var sheet = book.AddWorksheet();

        var textValue = "Write Test";

        sheet.Cell("A1").Value = textValue;

        using var stream = new MemoryStream();
        book.SaveAs(stream);

        var templateParts = stream.ToArray();
        
        var assembler = new ClosedXmlFileAssembler();

        // Act
        var fileContent = await assembler.AssembleAsync([templateParts]);

        using var resultStream = new MemoryStream(fileContent);
        using var resultBook = new XLWorkbook(resultStream);

        var resultValue = resultBook.Worksheet(1).Cell("A1").Value;

        // Assert
        Assert.True(fileContent.Length > 0);
        Assert.Equal(textValue, resultValue);
        
        // Для визуальной проверки
        File.WriteAllBytes(@"C:\Users\akolodka\Desktop\test.xlsx", fileContent);
    }
}
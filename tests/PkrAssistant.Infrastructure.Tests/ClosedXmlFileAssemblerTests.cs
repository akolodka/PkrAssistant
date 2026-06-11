using ClosedXML.Excel;
using PkrAssistant.Infrastructure;

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

        var referenceValue = "Write Test";

        using var book = new XLWorkbook();
        var sheet = book.AddWorksheet();

        sheet.Cell("A1").Value = referenceValue;

        using var stream = new MemoryStream();
        book.SaveAs(stream);

        var templateParts = stream.ToArray();
        
        var assembler = new ClosedXmlFileAssembler();

        // Act
        var fileContent = await assembler.AssembleAsync([templateParts]);

        using var resultStream = new MemoryStream(fileContent);
        using var resultBook = new XLWorkbook(resultStream);

        var readedValue = resultBook.Worksheet(1).Cell("A1").Value;

        // Assert
        Assert.True(fileContent.Length > 0);
        Assert.Equal(referenceValue, readedValue);
        
        // Для визуальной проверки
        // File.WriteAllBytes(@"C:\Users\akolodka\Desktop\test.xlsx", fileContent);
    }

    [Fact]
    public async Task AssembleAsync_WithVariableWidthColumns_Throws()
    {
        // Arrange
        var referenceValue = "Write Test";

        using var first = new XLWorkbook();

        first.AddWorksheet()
            .Cell("A1")
            .Value = referenceValue;

        using var firstStream = new MemoryStream();
        first.SaveAs(firstStream);

        using var second = new XLWorkbook();

        second.AddWorksheet()
            .Range("A1:C1")
            .Value = referenceValue;

        using var secondStream = new MemoryStream();
        second.SaveAs(secondStream);

        var templateParts = new [] { firstStream.ToArray(), secondStream.ToArray() };

        var assembler = new ClosedXmlFileAssembler();

        // Act + Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => assembler.AssembleAsync(templateParts));
    }
}